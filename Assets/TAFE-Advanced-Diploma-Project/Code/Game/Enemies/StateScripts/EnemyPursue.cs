using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/Pursue")]
public class EnemyPursue : EnemyState
{
    [SerializeField] private float lockOnTime = 3;
    [SerializeField] private float fireDelay = .25f;
    [SerializeField] private float speed = .5f;
    public override void OnEnter(Enemy _enemy)
    {
        base.OnEnter(_enemy);
        _enemy.Rigid.velocity = Vector2.zero;
        _enemy.Animator.SetTrigger("Aim");
    }

    public override void OnUpdate(Enemy _enemy)
    {
        base.OnUpdate(_enemy);
        if(!_enemy.KeepTime(Time.deltaTime, lockOnTime))
        {
            _enemy.transform.right = _enemy.GetDirectionToPlayer();
        }
        else if(_enemy.KeepTime(0, lockOnTime + fireDelay))
        {
            ChangeState(_enemy, typeof(EnemyAttack));
        }
    }

    public override void OnFixedUpdate(Enemy _enemy)
    {
        base.OnFixedUpdate(_enemy);
        _enemy.Rigid.velocity = -speed * _enemy.transform.right;
    }
}
