using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeInteractable : Interactable
{
    [SerializeField] bool DestroyOnPickUp;
    public override string GetPromptMessage()
    {
        return "Pick Up Axe";
    }

    public override void Interact()
    {
        WeaponManager.ChangeWeapon(new Axe());
        if (DestroyOnPickUp)
        {
            Destroy(gameObject);
        }

    }
}
