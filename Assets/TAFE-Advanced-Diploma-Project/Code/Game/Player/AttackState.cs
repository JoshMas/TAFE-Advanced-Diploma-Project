using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AbilityState
{
    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Animator.SetTrigger("Attack");
    }
}
