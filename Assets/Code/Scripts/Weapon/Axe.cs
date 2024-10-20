using UnityEngine;

public class Axe : Weapon
{
    public Axe()
    {
        weaponDamage = 35f;
        staminaConsumption = 16f;
        weaponRange = 5f;
        timeTillHit = 0.5f;
        IDLE = new AnimationClip(Animator.StringToHash("IdleAxe"), 0f);
        EQUIP = new AnimationClip(Animator.StringToHash("EquipAxe"), 0.43f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("UnequipAxe"), 0.43f);
        ATTACK = new AnimationClip(Animator.StringToHash("AttackAxe"), 0.94f);
        DEATH= new AnimationClip(Animator.StringToHash("Death_Axe"), 1f);
    }

    public override PlayerWeapon GetWeapon()
    {
        return PlayerWeapon.AXE;
    }
}
