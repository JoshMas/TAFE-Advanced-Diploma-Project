using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Stunned")]
public class StunnedState : AbilityState
{
    [SerializeField] private AnimationClip stunAnim;
    [SerializeField] private float gravityScale = .1f;
    private float oldGravityScale;

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
        oldGravityScale = player.Rigid.gravityScale;
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

    public override void OnExit()
    {
        base.OnExit();
        player.Rigid.gravityScale = oldGravityScale;
    }
}
