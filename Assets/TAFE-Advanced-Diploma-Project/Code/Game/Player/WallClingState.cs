using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/WallCling")]
public class WallClingState : AbilityState
{
    [SerializeField] private float slideSpeed = 1;
    private float climbValue;
    [SerializeField] private Vector2 wallJumpStrength;
    private bool hasJumped = false;

    private float timer2 = 0;
    [SerializeField] private float wallCheckInterval = .5f;

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
        climbValue = _walkSpeed.y;
    }

    public override void OnJump()
    {
        if (hasJumped)
            return;
        player.Rigid.AddForce(Vector3.up * wallJumpStrength.y + player.transform.right * wallJumpStrength.x, ForceMode2D.Impulse);
        hasJumped = true;
    }

    public override void OnUpdate()
    {
        if (!hasJumped)
        {
            player.Rigid.MovePosition(player.transform.position + Time.deltaTime * slideSpeed * climbValue * Vector3.up);
            timer2 += Time.deltaTime;
            if(timer2 > wallCheckInterval)
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
            timer += Time.deltaTime;
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
