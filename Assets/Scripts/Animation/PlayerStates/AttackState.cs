using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{
    public AttackState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.ATTACK)
        : base(key)
    {
        // TODO
        Id = 0;

    }

    public override void EnterState()
    {
        if (WeaponManager._currentWeapon != null)
        {
            if (Id != WeaponManager._currentWeapon.ATTACK.State)
            {
                Id = WeaponManager._currentWeapon.ATTACK.State;
                Duration = WeaponManager._currentWeapon.ATTACK.Duration;

            }
        }

        LockState();

    }

    public override void UpdateState()
    {


    }

    public override void ExitState()
    {

    }

    public override PlayerAnimationFSM.PlayerAnimation GetNextState()
    {
        // TODO
        
        if (PlayerMovementManager._isAttacking)
        {
            PlayerMovementManager._isAttacking = false;
        }

        return PlayerAnimationFSM.PlayerAnimation.IDLE;

    }
}
