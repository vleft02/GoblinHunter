using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Weapon
{
    public Hands()
    { 
        IDLE = new AnimationClip(Animator.StringToHash("Idle"), 0f);
        //EQUIP = new AnimationClip(Animator.StringToHash("Idle"), 0.6f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("changeWeapon"), 0.6f);
        ATTACK = new AnimationClip(Animator.StringToHash("Punch"), 0.6f);
        InitSequences();
    }
    public override string GetPromptMessage()
    {
        return "";
    }
    protected override void InitSequences()
    {
        _attackSequence = new List<AnimationClip>() { ATTACK };
    }
    public override void Interact()
    {
        _animator.ChangeWeapon(this);
    }

    public override PlayerWeapon GetWeapon()
    {
        return PlayerWeapon.HANDS;
    }

    protected override void EquipWeapon(){}
}
