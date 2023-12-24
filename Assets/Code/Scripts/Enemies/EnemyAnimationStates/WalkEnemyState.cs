using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkEnemyState : BaseAnimationState<EnemyAnimationFSM.EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> walkState = new Dictionary<Aspects, AnimationClip>();

    public WalkEnemyState(AnimationAspectManager _aspectManager, EnemyAnimationFSM.EnemyAnimation key = EnemyAnimationFSM.EnemyAnimation.WALK)
        : base(key)
    {
        // TODO
        Id = 0;

        AspectManager = _aspectManager;

        walkState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("WalkFront"), 0f);
        walkState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("WalkLeft"), 0f);
        walkState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("WalkRight"), 0f);
        walkState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("WalkBack"), 0f);

        walkState[Aspects.LEFT_FRONT] = new AnimationClip(Animator.StringToHash("WalkLeftFront"), 0f);
        walkState[Aspects.LEFT_BACK] = new AnimationClip(Animator.StringToHash("WalkLeftBack"), 0f);
        walkState[Aspects.RIGHT_FRONT] = new AnimationClip(Animator.StringToHash("WalkRightFront"), 0f);
        walkState[Aspects.RIGHT_BACK] = new AnimationClip(Animator.StringToHash("WalkRightBack"), 0f);
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
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(AspectManager._currentAspectKey);
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
        if (walkState[AspectManager._currentAspectKey].State != Id)
        {
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(AspectManager._currentAspectKey);
            AspectManager._changeAspect = true;
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
