using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : AbilityState
{
    [SerializeField] GameObject projectileTemplate;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
    }
}
