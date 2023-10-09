using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public Sword()
    {
        IDLE = new AnimationClip(Animator.StringToHash("idleSword"), 0f);
        EQUIP = new AnimationClip(Animator.StringToHash("toIdleSword"), 0.6f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("attackSword1"), 0.2f);
        ATTACK = new AnimationClip(Animator.StringToHash("attackSword2"), 0.5f);
        InitSequences();
    }

    public override string GetPromptMessage()
    {
        return "Press E to Pick Up Sword";
    }

    public override PlayerWeapon GetWeapon()
    {
        return PlayerWeapon.SWORD;
    }

    protected override void EquipWeapon()
    {
        GameObject _rightWeapon = GameObject.Find("Right Weapon");

        //Sword is set as a child of right Weapon holder
        //transforms initialized
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
        _attackSequence = new List<AnimationClip>() { UNEQUIP, ATTACK, EQUIP };
    }

}
