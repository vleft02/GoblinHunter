using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class IdleState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{
    public IdleState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.IDLE) 
        : base(key) 
    {
        Id = 0;
        
    }

    public override void EnterState()
    {
        WeaponManager._changeWeapon = false;
        if (WeaponManager._currentWeapon != null)
        {
            if (Id != WeaponManager._currentWeapon.IDLE.State)
            {
                Id = WeaponManager._currentWeapon.IDLE.State;
                Duration = WeaponManager._currentWeapon.IDLE.Duration;
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
        return PlayerAnimationFSM.PlayerAnimation.IDLE;
    }

}
