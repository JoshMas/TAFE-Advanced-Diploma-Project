using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Crouch")]
public class CrouchState : DefaultState
{
    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Crouch(true);
    }

    public override void OnCrouch(bool _isPressed)
    {
        if (!_isPressed)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        player.Crouch(false);
    }
}
