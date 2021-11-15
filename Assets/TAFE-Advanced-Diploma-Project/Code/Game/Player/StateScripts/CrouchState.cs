using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Crouch")]
public class CrouchState : DefaultState
{

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Animator.SetFloat("Crouch", 1);
    }

    public override void OnUpdate()
    {
        UpdateLoop();

        if (player.exactSpeedAxis.y != -1)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
