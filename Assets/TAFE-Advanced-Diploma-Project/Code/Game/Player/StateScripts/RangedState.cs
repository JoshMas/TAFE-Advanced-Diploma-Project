using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Ranged")]
public class RangedState : AbilityState
{
    [SerializeField] GameObject projectileTemplate;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        GameObject projectile = Instantiate(projectileTemplate, player.transform.position, player.transform.rotation);
        projectile.transform.right = player.GetMouseDirection();
        player.Animator.SetBool("Ranged", true);
    }

    public override void OnRanged(bool _isPressed)
    {
        if (!_isPressed)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        player.Animator.SetBool("Ranged", false);
    }
}
