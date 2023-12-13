using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{
    public EquipState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.EQUIP)
        : base(key)
    {
        // TODO
        Id = 0;

    }

    public override void EnterState()
    {
        if (WeaponManager._currentWeapon != null)
        {
            if (Id != WeaponManager._currentWeapon.EQUIP.State)
            {
                Id = WeaponManager._currentWeapon.EQUIP.State;
                Duration = WeaponManager._currentWeapon.EQUIP.Duration;
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
        
/*        if (PlayerMovementManager._isAttacking)
        {
            return PlayerAnimationFSM.PlayerAnimation.ATTACK;
        }
*/
        return PlayerAnimationFSM.PlayerAnimation.IDLE;
    }
}
