using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponAnimationFactory
{
    //private Weapon _currentWeaponAnimation;

    public static Weapon createWeaponAnimation(PlayerWeapon weapon)
    {
        if (weapon == PlayerWeapon.HANDS)
        {
            return new Hands();
        }
        else if (weapon == PlayerWeapon.SWORD)
        {
            return new Sword();
        }

        return null;
    }
    public static Weapon getWeaponAnimation(PlayerWeapon weapon)
    {
        return createWeaponAnimation(weapon);
     
    }

}
