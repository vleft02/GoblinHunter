using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public Axe()
    {
        IDLE = new AnimationClip(Animator.StringToHash("IdleAxe"), 0f);
        EQUIP = new AnimationClip(Animator.StringToHash("EquipAxe"), 1f);
        ATTACK = new AnimationClip(Animator.StringToHash("AttackAxe"), 1f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("UnequipAxe"), 1f);
        InitSequences();
    }

    public override string GetPromptMessage()
    {
        return "Press E to Pick Up Axe";
    }

    public override PlayerWeapon GetWeapon()
    {
        return PlayerWeapon.AXE;
    }

    protected override void EquipWeapon()
    {
        //Unequip Weapon Could possilbly move it behind camera instead of using Destroy
        GameObject _rightWeapon = GameObject.Find("Right Weapon");

        transform.SetParent(_rightWeapon.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(1f, 1f, 1f);

        //Disable Weapon collider So It is no longer Interactable
        GetComponent<Collider>().enabled = false;
    }

    public override void Interact()
    {
        _animator.ChangeWeapon(this);
    }

    protected override void InitSequences()
    {
        _attackSequence = new List<AnimationClip>() { ATTACK };
    }
}
