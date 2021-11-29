using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerStates/Fastfall")]
public class FastfallState : AbilityState
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        //player.Rigid.MovePosition(player.transform.position + moveSpeed * Time.deltaTime * Vector3.down);
        //if (player.IsGrounded())
        //{
        //    ChangeState(typeof(AttackState));
        //}
        timer += Time.deltaTime;
        if (timer > .1f)
        {
            timer = 0;
            if (player.IsGrounded())
            {
                ChangeState(typeof(AttackState));
            }
        }
    }
}
