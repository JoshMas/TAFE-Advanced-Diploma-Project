using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/Attack")]
public class EnemyAttack : EnemyState
{
    [SerializeField] private float duration = 1;
    [SerializeField] private float speed = 5;

    public override void OnEnter(Enemy _enemy)
    {
        base.OnEnter(_enemy);
        _enemy.Animator.SetTrigger("Attack");
        _enemy.Rigid.velocity = speed * _enemy.transform.right; 
    }

    public override void OnUpdate(Enemy _enemy)
    {
        base.OnUpdate(_enemy);
        if(_enemy.KeepTime(Time.deltaTime, duration))
        {
            ChangeState(_enemy, typeof(EnemyIdle));
        }
    }
}
