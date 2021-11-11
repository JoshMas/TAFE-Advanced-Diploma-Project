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

    [SerializeField] private float energyGain = 25;


    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Animator.SetFloat("Crouch", 0);
        midairJumpCounter = 0;
    }

    public override void OnUpdate()
    {
        UpdateLoop();

        if(player.exactSpeedAxis.y == -1)
        {
            ChangeState(typeof(CrouchState));
        }

        timer += Time.deltaTime;
        if (timer > .05f)
        {
            if (player.IsWallClinging())
            {
                ChangeState(typeof(WallClingState));
            }
            timer = 0;
        }
    }

    protected void UpdateLoop()
    {
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

        player.Energy.Charge(energyGain * Time.deltaTime);
    }

    public override void OnFixedUpdate()
    {
        Vector2 targetSpeed = new Vector2(player.lerpSpeedAxis.x * walkSpeed, player.Rigid.velocity.y);
        player.Rigid.velocity = targetSpeed;

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
