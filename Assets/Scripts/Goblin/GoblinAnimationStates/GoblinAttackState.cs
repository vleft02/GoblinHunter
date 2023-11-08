using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoblinAnimationFSM;

public class GoblinAttackState : BaseAnimationState<GoblinAnimation>
{
    private Dictionary<Aspects, AnimationClip> attackState = new Dictionary<Aspects, AnimationClip>();

    public GoblinAttackState(AnimationAspectManager _aspectManager, GoblinAnimation key = GoblinAnimation.ATTACK)
        : base(key)
    {
        // TODO
        Id = 0;
        AspectManager = _aspectManager;

        attackState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Hit"), 0f);
    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = attackState[aspect].State;
        Duration = attackState[aspect].Duration;
    }

    public override void EnterState()
    {
        ChangeAnimation(Aspects.FRONT);

    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {

    }

    public override GoblinAnimation GetNextState()
    {
        // TODO
        return GoblinAnimation.IDLE;

    }
}
