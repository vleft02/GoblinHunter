using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Weapon
{
    public Hands()
    {
        weaponDamage = 5f;
        staminaConsumption = 5f;
        weaponRange = 2f;
        IDLE = new AnimationClip(Animator.StringToHash("Idle"), 0f);
        //EQUIP = new AnimationClip(Animator.StringToHash("Idle"), 0.6f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("Unequip"), 0.3f);
        ATTACK = new AnimationClip(Animator.StringToHash("Punch"), 0.72f);
    }

    public override PlayerWeapon getWeapon()
    {
        return PlayerWeapon.HANDS;
    }

}
