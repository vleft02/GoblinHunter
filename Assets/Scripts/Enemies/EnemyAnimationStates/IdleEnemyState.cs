using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : BaseAnimationState<EnemyAnimationFSM.EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> idleState = new Dictionary<Aspects, AnimationClip>();

    public IdleEnemyState(EnemyAnimationFSM.EnemyAnimation key = EnemyAnimationFSM.EnemyAnimation.IDLE)
        : base(key)
    {
        // TODO
        Id = 0;

        idleState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Idle"), 0f);
        idleState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("IdleLeft"), 0f);
        idleState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("IdleRight"), 0f);
        idleState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("IdleBack"), 0f);

    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = idleState[aspect].State;
        Duration = idleState[aspect].Duration;
    }

    public override void EnterState()
    {
        if (idleState[AnimationAspectManager._currentAspectKey].State != Id)
        {
            Debug.Log("Change Aspect to " + AnimationAspectManager._currentAspectKey);
            ChangeAnimation(AnimationAspectManager._currentAspectKey);
        }


        //TODO
        //if (WeaponManager._currentWeapon != null)
        //{
        //    if (Id != WeaponManager._currentWeapon.ATTACK.State)
        //    {
        //        Id = WeaponManager._currentWeapon.ATTACK.State;
        //        Duration = WeaponManager._currentWeapon.ATTACK.Duration;

        //    }
        //}

        //LockState();

    }

    public override void UpdateState()
    {
        if (idleState[AnimationAspectManager._currentAspectKey].State != Id)
        {
            Debug.Log("Change Aspect to " + AnimationAspectManager._currentAspectKey);
            ChangeAnimation(AnimationAspectManager._currentAspectKey);
            AnimationAspectManager._changeAspect = true;
        }

    }

    public override void ExitState()
    {

    }

    public override EnemyAnimationFSM.EnemyAnimation GetNextState()
    {
        // TODO

        return EnemyAnimationFSM.EnemyAnimation.IDLE;

    }
}
