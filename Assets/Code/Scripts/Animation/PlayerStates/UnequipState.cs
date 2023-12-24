using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnequipState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{
    public UnequipState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.UNEQUIP)
        : base(key)
    {
        // TODO
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

/*        if (WeaponManager._changeWeapon)
        {
            WeaponManager._changeWeapon = false;
        }*/

        // TODO
        ////Unequip Weapon Could possilbly move it behind camera instead of using Destroy
        //GameObject _leftWeapon = GameObject.Find("Left Weapon");
        //GameObject _rightWeapon = GameObject.Find("Right Weapon");

        //if (_leftWeapon.transform.childCount > 0)
        //{
        //    Destroy(_leftWeapon.transform.GetChild(0).gameObject);
        //}
        //if (_rightWeapon.transform.childCount > 0)
        //{
        //    Destroy(_rightWeapon.transform.GetChild(0).gameObject);
        //}
    }

    public override PlayerAnimationFSM.PlayerAnimation GetNextState()
    {
        // TODO
        
/*        if (PlayerMovementManager._isAttacking)
        {
            return PlayerAnimationFSM.PlayerAnimation.ATTACK;
        }
*/
/*        if (WeaponManager._changeWeapon)
        {*/
            //Debug.Log("UNEQUIP go to next EQUIP");

            return PlayerAnimationFSM.PlayerAnimation.EQUIP;
/*        }*/
/*
        //Debug.Log("UNEQUIP go to next IDLE");
        return PlayerAnimationFSM.PlayerAnimation.IDLE;*/
    }
}
