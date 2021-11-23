using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerStates/Fastfall")]
public class FastfallState : AbilityState
{
    [SerializeField] private Type stateType;
    public override void OnUpdate()
    {
        base.OnUpdate();
        timer += Time.deltaTime;
        if(timer > .1f)
        {
            if (player.IsGrounded())
            {
                ChangeState(stateType);
            }
        }
    }
}
