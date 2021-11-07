using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Default")]
public class DefaultState : AbilityState
{
    [SerializeField] private int maxMidairJumps = 1;
    [SerializeField] private float walkSpeed = 8;
    private int midairJumpCounter = 0;

    private float midairJumpTimer = 0;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        midairJumpCounter = 0;
    }

    public override void OnUpdate()
    {
        UpdateLoop();

        if(player.targetSpeedAxis.y == -1)
        {
            ChangeState(typeof(CrouchState));
        }
    }

    protected void UpdateLoop()
    {
        timer += Time.deltaTime;
        if (timer > .1f)
        {
            if (player.IsWallClinging())
            {
                ChangeState(typeof(WallClingState));
            }
        }

        if (midairJumpCounter >= maxMidairJumps)
        {
            midairJumpTimer += Time.deltaTime;
            if (midairJumpTimer > .1f)
            {
                midairJumpTimer = 0;
                if (player.IsGrounded())
                    midairJumpCounter = 0;
            }
        }
    }

    public override void OnFixedUpdate()
    {
        Vector2 targetSpeed = new Vector2(player.targetSpeedAxis.x * walkSpeed, player.Rigid.velocity.y);
        player.Rigid.velocity = Vector2.Lerp(player.Rigid.velocity, targetSpeed, 10 * Time.deltaTime);

        if (targetSpeed.x > 0)
        {
            player.transform.forward = Vector3.forward;
        }
        else if (targetSpeed.x < 0)
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
        else if(maxMidairJumps > midairJumpCounter)
        {
            player.Rigid.velocity = new Vector2(player.Rigid.velocity.x, player.JumpStrength);
            ++midairJumpCounter;
        }
    }

    public override void OnAttack()
    {
        ChangeState(typeof(AttackState));
    }

    public override void OnAbilityOne()
    {
        ChangeState(typeof(DashState));
    }

    public override void OnAbilityTwo(bool _isPressed)
    {
        if (_isPressed)
        {
            ChangeState(typeof(GrappleState));
        }
    }
}
