using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeInteractable : Interactable
{
    public override string GetPromptMessage()
    {
        return "";
    }

    public override void Interact()
    {
        WeaponManager.ChangeWeapon(new Axe());
    }
}
