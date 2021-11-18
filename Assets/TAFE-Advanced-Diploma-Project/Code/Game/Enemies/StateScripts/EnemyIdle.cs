using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/Idle")]
public class EnemyIdle : EnemyState
{
    [SerializeField] private float force = 10;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float detectionRadius = 5;
    [SerializeField] private float idleTime = 2;

    public override void OnUpdate(Enemy _enemy)
    {
        base.OnUpdate(_enemy);
        if (_enemy.KeepTime(Time.deltaTime, idleTime) && _enemy.PlayerInLineOfSight(detectionRadius))
        {
            ChangeState(_enemy, typeof(EnemyPursue));
        }
    }

    public override void OnFixedUpdate(Enemy _enemy)
    {
        base.OnFixedUpdate(_enemy);
        Vector2 dir1 = (_enemy.OriginalPosition - (Vector2)_enemy.transform.position);
        Vector2 dir2 = Random.insideUnitCircle.normalized;
        if(dir1.magnitude > 0)
        {
            _enemy.Rigid.AddForce(force * (dir1 + dir2));
        }
        else
        {
            _enemy.Rigid.AddForce(force * dir2);
        }
        _enemy.Rigid.velocity = Vector2.ClampMagnitude(_enemy.Rigid.velocity, maxSpeed);
    }
}
