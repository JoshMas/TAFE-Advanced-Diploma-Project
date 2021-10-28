using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Default")]
public class DefaultState : AbilityState
{

    public override void OnUpdate()
    {
        Vector2 targetSpeed = new Vector2(player.targetSpeedAxis * player.WalkSpeed, player.Rigid.velocity.y);
        player.Rigid.velocity = Vector2.Lerp(player.Rigid.velocity, targetSpeed, 10 * Time.deltaTime);
        player.Charge();

        if (targetSpeed.x > 0)
        {
            player.transform.forward = Vector3.forward;
        }
        else if (targetSpeed.x < 0)
        {
            player.transform.forward = Vector3.back;
        }

        timer += Time.deltaTime;
        if (timer > abilityTimeKeeper)
        {
            if (player.IsWallClinging())
            {
                ChangeState(typeof(WallClingState));
            }
        }
    }

    public override void OnWalk(float _walkSpeed)
    {
        base.OnWalk(_walkSpeed);
        
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
        if (_isPressed)
        {
            ChangeState(typeof(CrouchState));
        }
    }

    public override void OnAbilityOne()
    {
        ChangeState(typeof(DashState));
    }
}
