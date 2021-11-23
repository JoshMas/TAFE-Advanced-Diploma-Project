using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Stunned")]
public class StunnedState : AbilityState
{
    [SerializeField] private AnimationClip stunAnim;

    private void OnValidate()
    {
        if(stunAnim != null)
        {
            abilityTimeKeeper = stunAnim.length;
        }
    }

    public override void OnEnter(Player _player)
    {
        base.OnEnter(_player);
        player.Animator.SetTrigger("Damage");
        player.Rigid.gravityScale = gravityScale;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        timer += Time.deltaTime;
        if(timer >= abilityTimeKeeper)
        {
            ChangeState(typeof(DefaultState));
        }
    }

    public override void OnFixedUpdate() { }
}
