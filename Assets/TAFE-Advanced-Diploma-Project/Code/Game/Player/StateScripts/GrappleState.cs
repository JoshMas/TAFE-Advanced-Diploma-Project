using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Grapple")]
public class GrappleState : AbilityState
{
    [SerializeField] private float grappleRange = 10;
    [SerializeField] private float grappleSpeed = 10;

    private Vector2 targetLocation = Vector2.zero;
    private bool targetFound = false;
    private Transform targetTransform;
    private bool attached = false;

    [SerializeField] private bool autoVersion = false;
    private bool canJump = false;
    [SerializeField] private float jumpAngle = 45;
    private bool jumped = false;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);

        if (player.Energy.HasEnergy(abilityCost))
        {
            player.Energy.Charge(-abilityCost);
        }
        else if (player.Health.HasEnergy(abilityCost))
        {
            player.Energy.Charge(-abilityCost);
            player.Health.TakeDamage(abilityCost / 2, 1);
        }
        else
        {
            ChangeState(typeof(DefaultState));
            return;
        }

        targetLocation = GetTargetLocation();
        player.GrappleActive(true);
        player.UpdateGrapplePosition(player.transform.position);
    }

    public override void OnUpdate()
    {
        if (autoVersion)
        {
            if (jumped)
            {
                timer += Time.deltaTime;
                if(timer > .5f)
                {
                    ChangeState(typeof(DefaultState));
                }
                return;
            }
            if (attached)
            {
                if(!canJump && player.GetGrappleAngle() < jumpAngle)
                {
                    canJump = true;
                }

                if(canJump && player.GetGrappleAngle() > jumpAngle)
                {
                    jumped = true;
                    player.Rigid.AddForce(player.GetGrappleVector() * moveSpeed, ForceMode2D.Impulse);
                }
            }
        }
        if (attached)
        {
            player.UpdateGrapplePosition(player.Grapple.position);
            return;
        }
        if((Vector2)player.Grapple.position == targetLocation)
        {
            if (targetFound)
            {
                player.GrappleAttach(true);
                player.ReparentGrapple(targetTransform);
                attached = true;
            }
            else
            {
                ChangeState(typeof(DefaultState));
            }
        }
        else
        {
            player.UpdateGrapplePosition(Vector2.MoveTowards(player.Grapple.position, targetLocation, grappleSpeed * Time.deltaTime));
        }
    }

    public override void OnFixedUpdate()
    {
        if (attached && !jumped)
        {
            if (autoVersion)
            {
                player.Rigid.velocity = player.GetGrappleVector() * moveSpeed;
            }
            else
            {
                player.Rigid.AddForce(player.exactSpeedAxis * moveSpeed * Vector2.right);
            }
        }
    }

    public override void OnAbilityTwo(bool _isPressed)
    {
        if (!_isPressed)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        attached = false;
        targetFound = false;
        targetLocation = Vector2.zero;
        targetTransform = null;
        player.ReparentGrapple(null);
        player.UpdateGrapplePosition(player.transform.position);
        player.GrappleAttach(false);
        player.GrappleActive(false);

        canJump = false;
        jumped = false;
    }

    private Vector2 GetTargetLocation()
    {
        Vector2 direction = player.GetMouseDirection();
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction, grappleRange, player.GroundMask);
        if(hit.transform != null)
        {
            targetFound = true;
            targetTransform = hit.transform;
            return hit.point;
        }
        else
        {
            return (Vector2)player.transform.position + direction * grappleRange;
        }
    }
}
