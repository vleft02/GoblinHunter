using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{
    public UnequipState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.UNEQUIP)
        : base(key)
    {
        Id = 0;

    }

    public override void EnterState()
    {
        if (WeaponManager._currentWeapon != null)
        {
            if (Id != WeaponManager._previousWeapon.UNEQUIP.State)
            {
                Id = WeaponManager._previousWeapon.UNEQUIP.State;
                Duration = WeaponManager._previousWeapon.UNEQUIP.Duration;
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
        return PlayerAnimationFSM.PlayerAnimation.EQUIP;
    }
}
