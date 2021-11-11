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
        Instantiate(projectileTemplate, player.transform.position, player.transform.rotation);
    }

    public override void OnRanged(bool _isPressed)
    {
        if (!_isPressed)
        {
            ChangeState(typeof(DefaultState));
        }
    }
}
