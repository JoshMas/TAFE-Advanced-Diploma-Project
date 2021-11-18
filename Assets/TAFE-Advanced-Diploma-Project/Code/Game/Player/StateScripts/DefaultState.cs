using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Default")]
public class DefaultState : AbilityState
{
    [SerializeField] private int maxMidairJumps = 1;
    private int midairJumpCounter = 0;
    private bool isMidair = false;
    private float midairJumpTimer = 0;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Animator.SetFloat("Crouch", 0);
        midairJumpCounter = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (isMidair)
        {
            midairJumpTimer += Time.deltaTime;
            if (midairJumpTimer > .1f)
            {
                midairJumpTimer = 0;
                if (player.IsGrounded())
                {
                    isMidair = false;
                    midairJumpCounter = 0;
                }
            }
        }
        else
        {
            midairJumpTimer += Time.deltaTime;
            if (midairJumpTimer > .1f)
            {
                midairJumpTimer = 0;
                if (!player.IsGrounded())
                {
                    isMidair = true;
                }
            }
        }
        player.Animator.SetBool("Midair", isMidair);
        player.Energy.Charge(energyGain * Time.deltaTime);

        StateTransitions();
    }

    protected virtual void StateTransitions()
    {
        if (player.exactSpeedAxis.y == -1)
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

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        if (player.Rigid.velocity.x > 0)
        {
            player.transform.forward = Vector3.forward;
        }
        else if (player.Rigid.velocity.x < 0)
        {
            player.transform.forward = Vector3.back;
        }
        
    }

    public override void OnJump()
    {
        if (player.IsGrounded())
        {
            player.Rigid.velocity = new Vector2(player.Rigid.velocity.x, player.JumpStrength);
            player.Animator.SetTrigger("Jumped");
        }
        else if(maxMidairJumps > midairJumpCounter)
        {
            player.Rigid.velocity = new Vector2(player.Rigid.velocity.x, player.JumpStrength);
            ++midairJumpCounter;
            player.Animator.SetTrigger("Jumped");
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
