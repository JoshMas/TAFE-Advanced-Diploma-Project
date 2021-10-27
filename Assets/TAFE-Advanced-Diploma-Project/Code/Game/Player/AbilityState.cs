using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityState
{
    private Player player;

    protected virtual void OnEnter() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnExit() { }
}
