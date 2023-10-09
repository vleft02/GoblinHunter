using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Daggers: Weapon
{
    public Daggers()
    {
        IDLE = new AnimationClip(Animator.StringToHash("idle dagger"), 0f);
        EQUIP = new AnimationClip(Animator.StringToHash("equipDaggers"), 1f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("unequipDaggers"), 0.5f);
        ATTACK = new AnimationClip(Animator.StringToHash("attackDagger"), 0.82f);
        InitSequences();
    }

    public override string GetPromptMessage()
    {
        return "Press E to Pick Up Daggers";
    }

    public override PlayerWeapon GetWeapon()
    {
        return PlayerWeapon.DAGGERS;
    }

    protected override void EquipWeapon()
    {
        GameObject _leftDagger = GameObject.FindGameObjectWithTag("Left Dagger");
        GameObject _rightDagger = GameObject.FindGameObjectWithTag("Right Dagger");

        GameObject _leftWeaponHolder = GameObject.Find("Left Weapon");
        GameObject _rightWeaponHolder = GameObject.Find("Right Weapon");
       
        //daggers are set as children of right and left Weapon holder respectively
        //transforms initialized
        //AN TA ALLAKSETE TO ROTATION MPOREI NA XALASOUN TA DAGGERS
        _leftDagger.transform.SetParent(_leftWeaponHolder.transform);
        _rightDagger.transform.SetParent(_rightWeaponHolder.transform);
        _leftDagger.transform.localPosition = Vector3.zero;
        _rightDagger.transform.localPosition = Vector3.zero;
        _leftDagger.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _rightDagger.transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        /*transform.localScale = Vector3.one;*/


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
