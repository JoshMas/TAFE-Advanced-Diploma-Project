using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityState : ScriptableObject
{
    protected Player player;
    [SerializeField] protected AbilityState[] transitions;

    [SerializeField] protected float abilityDuration = 1;
    protected float timer = 0;
    [SerializeField] protected float abilityCost = 10;

    public virtual void OnEnter(Player _player) { player = _player; }
    public virtual void OnUpdate() { }
    public virtual void OnJump() { }
    public virtual void OnCrouch(bool _isPressed) { }
    public virtual void OnWalk(float _walkSpeed) { }
    public virtual void OnAbilityOne() { }
    public virtual void OnAbilityTwo() { }
    public virtual void OnExit() { timer = 0; }
}
