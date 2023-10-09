using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public record AnimationClip(int State, float Duration);

public abstract class Weapon : MonoBehaviour, Interactable
{
    protected PlayerAnimator _animator;
    protected PlayerWeapon _playerWeapon;
    public List<AnimationClip> _attackSequence { get; protected set; }
    
    // Animations
    public AnimationClip IDLE { get; protected set; }
    public AnimationClip EQUIP { get; protected set; }
    public AnimationClip UNEQUIP { get; protected set; }
    public AnimationClip ATTACK { get; protected set; }

    protected abstract void InitSequences();
    public abstract void Interact();
    public abstract PlayerWeapon GetWeapon();

    public abstract string GetPromptMessage();

    //Each Weapon initializes transforms
    //gets equipped in to the appropriate weapon Holder
    //Unequips Previous Weapon
    public void InitializeWeapon()
    {
        UnequipPreviousWeapon();
        EquipWeapon();
    }
    void Start()
    {
       _animator =  GameObject.Find("Player").GetComponent<PlayerAnimator>();
    }

    private void UnequipPreviousWeapon()
    {
        //Unequip Weapon Could possilbly move it behind camera instead of using Destroy
        GameObject _leftWeapon = GameObject.Find("Left Weapon");
        GameObject _rightWeapon = GameObject.Find("Right Weapon");

        if (_leftWeapon.transform.childCount > 0)
        {
            Destroy(_leftWeapon.transform.GetChild(0).gameObject);
        }
        if (_rightWeapon.transform.childCount > 0)
        {
            Destroy(_rightWeapon.transform.GetChild(0).gameObject);
        }
    }
    protected abstract void EquipWeapon();
}
