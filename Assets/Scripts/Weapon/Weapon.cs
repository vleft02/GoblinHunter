using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{


    // Animations
    public AnimationClip IDLE { get; protected set; }
    public AnimationClip EQUIP { get; protected set; }
    public AnimationClip UNEQUIP { get; protected set; }
    public AnimationClip ATTACK { get; protected set; }

    protected Weapon()
    {

    }

    public abstract PlayerWeapon getWeapon();
    


}
