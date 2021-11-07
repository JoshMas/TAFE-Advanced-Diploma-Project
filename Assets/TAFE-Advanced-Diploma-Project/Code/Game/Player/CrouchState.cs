using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Crouch")]
public class CrouchState : DefaultState
{
    [SerializeField] private float energyGain = 25;

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Animator.SetFloat("Crouch", 1);
    }

    public override void OnUpdate()
    {
        UpdateLoop();

        if (player.targetSpeedAxis.y != -1)
        {
            ChangeState(typeof(DefaultState));
        }

        player.Energy.Charge(energyGain * Time.deltaTime);
    }

    public override void OnExit()
    {
        base.OnExit();
        player.Animator.SetFloat("Crouch", 0);
    }
}
