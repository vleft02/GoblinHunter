using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAnimationFSM;

public class EnemyToDeathState : BaseAnimationState<EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> toDeathState = new Dictionary<Aspects, AnimationClip>();

    public EnemyToDeathState(AnimationAspectManager _aspectManager, EnemyAnimation key = EnemyAnimation.TO_DEATH)
        : base(key)
    {
        Id = 0;

        AspectManager = _aspectManager;

        toDeathState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("ToDeath"), 0f);


    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = toDeathState[aspect].State;
        Duration = toDeathState[aspect].Duration;
    }

    public override void EnterState()
    {
        if (toDeathState[Aspects.FRONT].State != Id)
        {
            ChangeAnimation(Aspects.FRONT);
        }

    }

    public override void UpdateState()
    {


    }

    public override void ExitState()
    {

    }

    public override EnemyAnimation GetNextState()
    {
        return EnemyAnimation.TO_DEATH;

    }
}
