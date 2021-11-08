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

    public virtual void OnEnter(Player _player) { player = _player; }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnAttack() { }
    public virtual void OnRanged() { }
    public virtual void OnInstall() { }
    public virtual void OnJump() { }
    public virtual void OnMove() { }
    public virtual void OnAbilityOne() { }
    public virtual void OnAbilityTwo(bool _isPressed) { }
    public virtual void OnExit() { timer = 0; }

    /// <summary>
    /// Pass the type of state you want, and if it's in the array of valid transistions, swap to that state
    /// It will always use the first one it finds
    /// </summary>
    /// <param name="t"></param>
    protected void ChangeState(Type t)
    {
        foreach(AbilityState transition in transitions)
        {
            if (transition.GetType() == t)
            {
                player.ChangeState(transition);
            }
        }
    }
}
