using UnityEngine;

public class Sword : Weapon
{
    public Sword()
    {
        weaponDamage = 25f;
        staminaConsumption = 10f;
        weaponRange = 5f;
        timeTillHit = 0.31f;
        IDLE = new AnimationClip(Animator.StringToHash("IdleSword"), 0f);
        EQUIP = new AnimationClip(Animator.StringToHash("EquipSword"), 0.43f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("UnequipSword"), 0.43f);
        ATTACK = new AnimationClip(Animator.StringToHash("AttackSword"), 0.94f);
        DEATH = new AnimationClip(Animator.StringToHash("Death_sword"), 1f);
    }

    public override PlayerWeapon GetWeapon()
    {
        return PlayerWeapon.SWORD;
    }
}
