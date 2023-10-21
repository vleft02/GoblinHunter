using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkEnemyState : BaseAnimationState<EnemyAnimationFSM.EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> walkState = new Dictionary<Aspects, AnimationClip>();

    public WalkEnemyState(EnemyAnimationFSM.EnemyAnimation key = EnemyAnimationFSM.EnemyAnimation.WALK)
        : base(key)
    {
        // TODO
        Id = 0;

        walkState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("WalkFront"), 0f);
        walkState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("WalkRight"), 0f);
        walkState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("WalkLeft"), 0f);
        walkState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("WalkFront"), 0f);



    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = walkState[aspect].State;
        Duration = walkState[aspect].Duration;
    }

    public override void EnterState()
    {
        if (walkState[AnimationAspectManager._currentAspectKey].State != Id)
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
        if (walkState[AnimationAspectManager._currentAspectKey].State != Id)
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

        return EnemyAnimationFSM.EnemyAnimation.WALK;

    }
}
