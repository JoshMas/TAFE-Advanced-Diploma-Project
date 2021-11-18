using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make sure that no information is altered at runtime in these states.
/// I'm pretty sure that will make them do weird things when there's multiple enemies
/// </summary>
public class EnemyState : ScriptableObject
{
    [SerializeField] protected EnemyState[] transitions;
    public virtual void OnEnter(Enemy _enemy) { }
    public virtual void OnUpdate(Enemy _enemy) { }
    public virtual void OnFixedUpdate(Enemy _enemy) { }
    public virtual void OnExit(Enemy _enemy) { }

    public void ChangeState(Enemy _enemy, Type t)
    {
        foreach(EnemyState transition in transitions)
        {
            if(transition.GetType() == t)
            {
                _enemy.ChangeState(transition);
                return;
            }
        }
    }
}
