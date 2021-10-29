using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/WallCling")]
public class WallClingState : AbilityState
{
    [SerializeField] private float slideSpeed = 1;
    private bool hasJumped = false;

    private float timer2 = 0;
    [SerializeField] private float wallCheckInterval = .5f;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Rigid.velocity = Vector2.zero;
        player.transform.forward = -player.transform.forward;
    }

    public override void OnMove(float _walkSpeed)
    {
        base.OnMove(_walkSpeed);
        if (_walkSpeed == player.transform.right.x)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnJump()
    {
        player.Rigid.AddForce(Vector3.up * player.JumpStrength + player.transform.right * 5, ForceMode2D.Impulse);
        hasJumped = true;
    }

    public override void OnUpdate()
    {
        if (!hasJumped)
        {
            player.Rigid.MovePosition(player.transform.position + Time.deltaTime * slideSpeed * Vector3.down);
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
    }
}
