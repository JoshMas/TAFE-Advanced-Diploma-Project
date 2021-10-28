using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Dash")]
public class DashState : AbilityState
{

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        if (player.Energy.HasEnergy())
        {
            player.Energy.Spend(abilityCost);
        }
        else
        {
            player.ChangeState(transitions[0]);
        }
    }

    public override void OnUpdate()
    {
        player.Rigid.MovePosition(player.transform.position + player.transform.right);
        timer += Time.deltaTime;
        if(timer >= abilityDuration)
        {
            player.ChangeState(transitions[0]);
        }
    }
}
