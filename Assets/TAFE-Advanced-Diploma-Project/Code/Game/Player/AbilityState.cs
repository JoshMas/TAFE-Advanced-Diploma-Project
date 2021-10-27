using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityState : ScriptableObject
{
    private Player player;

    protected virtual void OnEnter(Player _player) { player = _player; }
    protected virtual void OnUpdate() { }
    protected virtual void OnExit() { }
}
