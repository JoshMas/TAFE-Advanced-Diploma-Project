using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/WallCling")]
public class WallClingState : AbilityState
{
    [SerializeField] private float slideSpeed = 1;
    [SerializeField] private Vector2 wallJumpStrength;
    private bool hasJumped = false;

    private float timer2 = 0;

    private float gravityScale = 0;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Rigid.velocity = Vector2.zero;
        player.transform.forward = -player.transform.forward;
        gravityScale = player.Rigid.gravityScale;
        player.Rigid.gravityScale = 0;
    }

    public override void OnMove(Vector2 _walkSpeed)
    {
        base.OnMove(_walkSpeed);
        if (_walkSpeed.x == player.transform.right.x)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnJump()
    {
        if (hasJumped)
            return;
        player.Rigid.AddForce(Vector3.up * wallJumpStrength.y + player.transform.right * wallJumpStrength.x, ForceMode2D.Impulse);
        hasJumped = true;
    }

    public override void OnFixedUpdate()
    {
        if (!hasJumped)
        {
            player.Rigid.MovePosition(player.transform.position + Time.deltaTime * slideSpeed * player.targetSpeedAxis.y * Vector3.up);
            timer2 += Time.fixedDeltaTime;
            if(timer2 > .1f)
            {
                timer2 = 0;
                if (!player.IsStillWallClinging())
                {
                    ChangeState(typeof(DefaultState));
                }
            }
        }
        else
        {
            timer += Time.fixedDeltaTime;
            if(timer >= abilityTimeKeeper)
            {
                ChangeState(typeof(DefaultState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        hasJumped = false;
        player.Rigid.gravityScale = gravityScale;
    }
}
