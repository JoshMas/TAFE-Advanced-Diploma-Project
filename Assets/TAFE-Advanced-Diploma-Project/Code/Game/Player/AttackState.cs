using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Attack")]
public class AttackState : AbilityState
{
    [SerializeField] private AnimationClip attack;
    [SerializeField] private AnimationClip recovery;
    [SerializeField] private bool comboEnder = false;

    private void OnEnable()
    {
        abilityTimeKeeper = attack.length + recovery.length;
    }

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Animator.SetTrigger("Attack");
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;
        if(timer > abilityTimeKeeper)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnAttack()
    {
        if (comboEnder)
            return;
        if(timer > attack.length)
        {
            ChangeState(typeof(AttackState));
        }
    }
}
