using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Dash")]
public class DashState : AbilityState
{
    private Vector2 dash;
    [SerializeField] private bool horizontalOnly = true;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        if (player.Energy.HasEnergy(abilityCost))
        {
            player.Energy.Charge(-abilityCost);
        }
        else if (player.Health.HasEnergy(abilityCost))
        {
            player.Energy.Charge(-abilityCost);
            player.Health.TakeDamage(abilityCost / 2, 1);
        }
        else
        {
            ChangeState(typeof(DefaultState));
            return;
        }

        if (horizontalOnly)
        {
            dash = moveSpeed * player.transform.right;
        }
        else
        {
            dash = moveSpeed * player.GetMouseDirection();
        }
        player.Animator.SetTrigger("Dash");
        player.Rigid.velocity = Vector2.zero;
    }

    public override void OnUpdate()
    {
        //player.Rigid.MovePosition(player.transform.position + Time.deltaTime * dashSpeed * player.transform.right);
        timer += Time.deltaTime;
        if(timer >= abilityTimeKeeper)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnFixedUpdate()
    {
        player.Rigid.MovePosition(player.Rigid.position + dash * Time.fixedDeltaTime);
    }

    public override void OnAttack()
    {
        ChangeState(typeof(AttackState));
    }

    public override void OnAbilityTwo(bool _isPressed)
    {
        if (_isPressed)
        {
            ChangeState(typeof(GrappleState));
        }
    }
}
