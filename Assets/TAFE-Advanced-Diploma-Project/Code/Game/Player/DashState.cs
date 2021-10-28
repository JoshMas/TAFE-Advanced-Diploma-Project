using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Dash")]
public class DashState : AbilityState
{
    [SerializeField] private float dashSpeed = 25;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Rigid.velocity = Vector2.zero;
        if (player.Energy.HasEnergy())
        {
            player.Energy.Spend(abilityCost);
        }
        else
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnUpdate()
    {
        player.Rigid.MovePosition(player.transform.position + Time.deltaTime * dashSpeed * player.transform.right);
        timer += Time.deltaTime;
        if(timer >= abilityTimeKeeper)
        {
            ChangeState(typeof(DefaultState));
        }
    }
}
