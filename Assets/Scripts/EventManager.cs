using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class EventManager /*: MonoBehaviour*/
{
    public static event Action AttackEvent;

    public static event Action EquipWeaponEvent;


    public static void AttackPerformed()
    {
        AttackEvent?.Invoke();
    }

    public static void EquipWeapon()
    {
        EquipWeaponEvent?.Invoke();
    }
}