using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Dash")]
public class DashState : AbilityState
{
    [SerializeField] private float dashSpeed = 25;
    [SerializeField] private bool horizontalOnly = true;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Rigid.velocity = Vector2.zero;
        if (player.Energy.HasEnergy())
        {
            player.Energy.Spend(abilityCost);
            if (horizontalOnly)
            {
                player.Rigid.velocity = dashSpeed * player.transform.right;
            }
            else
            {
                player.Rigid.velocity = dashSpeed * player.GetMouseDirection();
            }
        }
        else
        {
            ChangeState(typeof(DefaultState));
        }
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

    public override void OnAbilityTwo(bool _isPressed)
    {
        if (_isPressed)
        {
            ChangeState(typeof(GrappleState));
        }
    }
}
