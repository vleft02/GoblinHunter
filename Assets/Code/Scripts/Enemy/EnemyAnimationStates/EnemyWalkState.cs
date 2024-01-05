using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAnimationFSM;

public class EnemyWalkState : BaseAnimationState<EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> walkState = new Dictionary<Aspects, AnimationClip>();

    private EnemyStateMachine _stateMachine;

    public EnemyWalkState(AnimationAspectManager _aspectManager, EnemyStateMachine stateMachine, EnemyAnimation key = EnemyAnimation.WALK)
        : base(key)
    {
        Id = 0;

        AspectManager = _aspectManager;
        _stateMachine = stateMachine;

        walkState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("WalkFront"), 0f);
        walkState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("WalkLeft"), 0f);
        walkState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("WalkRight"), 0f);
        walkState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("WalkBack"), 0f);

        walkState[Aspects.RIGHT_FRONT] = new AnimationClip(Animator.StringToHash("WalkLeftFront"), 0f);
        walkState[Aspects.RIGHT_BACK] = new AnimationClip(Animator.StringToHash("WalkLeftBack"), 0f);
        walkState[Aspects.LEFT_FRONT] = new AnimationClip(Animator.StringToHash("WalkRightFront"), 0f);
        walkState[Aspects.LEFT_BACK] = new AnimationClip(Animator.StringToHash("WalkRightBack"), 0f);

    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = walkState[aspect].State;
        Duration = walkState[aspect].Duration;
    }

    public override void EnterState()
    {
        if (walkState[AspectManager._currentAspectKey].State != Id)
        {
            ChangeAnimation(AspectManager._currentAspectKey);
        }

    }

    public override void UpdateState()
    {
        if (_stateMachine.CurrentState.StateKey == EnemyStateMachine.EnemyState.CHASE)
        {
            AspectManager.disableAspects = true;
        }
        else
        {
            AspectManager.disableAspects = false;
        }

        if (walkState[AspectManager._currentAspectKey].State != Id)
        {
            ChangeAnimation(AspectManager._currentAspectKey);
            AspectManager._changeAspect = true;

        }

    }

    public override void ExitState()
    {
    
    }

    public override EnemyAnimation GetNextState()
    {
        if (_stateMachine.isAttacking)
        {
            return EnemyAnimation.ATTACK;
        }

        if (_stateMachine.Agent.velocity.magnitude > 0)
        {
            return EnemyAnimation.WALK;
        }

        return EnemyAnimation.IDLE;

    }
}
