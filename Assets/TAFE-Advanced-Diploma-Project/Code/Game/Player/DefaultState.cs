using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Default")]
public class DefaultState : AbilityState
{
    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
    }

    public override void OnUpdate()
    {
        player.Rigid.velocity = new Vector2(player.currentSpeed * player.WalkSpeed, player.Rigid.velocity.y);
        player.Charge();
    }

    public override void OnWalk(float _walkSpeed)
    {
        player.currentSpeed = _walkSpeed;
        if (_walkSpeed > 0)
        {
            player.transform.forward = Vector3.forward;
        }
        else if (_walkSpeed < 0)
        {
            player.transform.forward = Vector3.back;
        }
    }

    public override void OnJump()
    {
        if (player.IsGrounded())
        {
            player.Rigid.velocity = new Vector2(player.Rigid.velocity.x, player.JumpStrength);
        }
    }

    public override void OnCrouch(bool _isPressed)
    {
        player.Crouch(_isPressed);
    }

    public override void OnAbilityOne()
    {
        player.ChangeState(transitions[0]);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
