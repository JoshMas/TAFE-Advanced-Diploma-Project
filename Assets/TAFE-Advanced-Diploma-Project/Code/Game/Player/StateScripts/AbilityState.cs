using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityState : ScriptableObject
{
    protected Player player;
    [SerializeField] protected AbilityState[] transitions;

    [SerializeField] protected float abilityTimeKeeper = 1;
    protected float timer = 0;
    [SerializeField] protected float abilityCost = 10;
    [SerializeField] protected float moveSpeed = 0;
    [SerializeField] protected float energyGain = 0;
    [SerializeField] protected float gravityScale = 3;

    public virtual void OnEnter(Player _player) 
    { 
        player = _player;
        player.Rigid.gravityScale = gravityScale;
    }
    public virtual void OnUpdate()
    {
        player.Animator.SetFloat("xSpeed", Mathf.Abs(player.lerpSpeedAxis.x));
        player.Animator.SetFloat("ySpeed", Mathf.Clamp(.25f * player.Rigid.velocity.y, -1, 1));
        player.Energy.Charge(energyGain * Time.deltaTime);
    }
    public virtual void OnFixedUpdate()
    {
        Vector2 targetSpeed = new Vector2(player.lerpSpeedAxis.x * moveSpeed, player.Rigid.velocity.y);
        player.Rigid.velocity = targetSpeed;
    }
    public virtual void OnAttack() { }
    public virtual void OnRanged(bool _isPressed) { }
    public virtual void OnInstall() { }
    public virtual void OnJump() { }
    public virtual void OnMove() { }
    public virtual void OnAbilityOne() { }
    public virtual void OnAbilityTwo(bool _isPressed) { }
    public virtual void OnParry() { }
    public virtual void OnExit() { timer = 0; }

    /// <summary>
    /// Pass the type of state you want, and if it's in the array of valid transistions, swap to that state
    /// It will always use the first one it finds
    /// </summary>
    /// <param name="t"></param>
    public void ChangeState(Type t)
    {
        foreach(AbilityState transition in transitions)
        {
            if (transition.GetType() == t)
            {
                player.ChangeState(transition);
                return;
            }
        }
    }

    public void ChangeState(Type t, int num)
    {
        int counter = num;
        foreach(AbilityState transition in transitions)
        {
            if(transition.GetType() == t)
            {
                if(counter <= 0)
                {
                    player.ChangeState(transition);
                    return;
                }
                else
                {
                    counter--;
                }
            }
        }
    }
}
