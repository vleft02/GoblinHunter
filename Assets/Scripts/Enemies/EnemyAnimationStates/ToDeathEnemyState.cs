using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDeathEnemyState : BaseAnimationState<EnemyAnimationFSM.EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> toDeathState = new Dictionary<Aspects, AnimationClip>();

    public ToDeathEnemyState(AnimationAspectManager _aspectManager, EnemyAnimationFSM.EnemyAnimation key = EnemyAnimationFSM.EnemyAnimation.TO_DEATH)
        : base(key)
    {
        // TODO
        Id = 0;

        AspectManager = _aspectManager;

        toDeathState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Knight_ToDeathFront"), 0f);
        
        
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
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(Aspects.FRONT);
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


    }

    public override void ExitState()
    {

    }

    public override EnemyAnimationFSM.EnemyAnimation GetNextState()
    {
        // TODO
        return EnemyAnimationFSM.EnemyAnimation.TO_DEATH;

    }
}
