using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggersInteractable : Interactable
{
    public override string GetPromptMessage()
    {
        return "";
    }

    public override void Interact()
    {
        WeaponManager.ChangeWeapon(new Daggers());
    }
}
