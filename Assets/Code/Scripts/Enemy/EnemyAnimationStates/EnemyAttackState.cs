using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAnimationFSM;

public class EnemyAttackState : BaseAnimationState<EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> attackState = new Dictionary<Aspects, AnimationClip>();

    private EnemyStateMachine _stateMachine;

    public EnemyAttackState(AnimationAspectManager _aspectManager, EnemyStateMachine stateMachine, EnemyAnimation key = EnemyAnimation.ATTACK)
        : base(key)
    {
        Id = 0;
        AspectManager = _aspectManager;

        _stateMachine = stateMachine;

        attackState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Attack"), 1.25f);// 1.25-fast, 3-slow
    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = attackState[aspect].State;
        Duration = attackState[aspect].Duration;
    }

    public override void EnterState()
    {
        ChangeAnimation(Aspects.FRONT);

        _stateMachine.canEndAttack = false;

        LockState();
    }

    public override void UpdateState()
    {

        if (Time.time >= LockedStateTime)
        {
            if (_stateMachine.IsInCombatRange())
            {
                _stateMachine.canEndAttack = false;
                AspectManager._changeAspect = true;
                LockState();
            }
            
            
        }


    }

    public override void ExitState()
    {
    }

    public override EnemyAnimation GetNextState()
    {

        if (_stateMachine.Agent.velocity.magnitude > 0)
        {
            return EnemyAnimation.WALK;
        }

        return EnemyAnimation.ATTACK;

    }
}
