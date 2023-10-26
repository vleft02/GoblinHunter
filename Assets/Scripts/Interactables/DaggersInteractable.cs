using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggersInteractable : Interactable
{
    [SerializeField] bool DestroyOnPickUp;
    public override string GetPromptMessage()
    {
        return "";
    }

    public override void Interact()
    {
        WeaponManager.ChangeWeapon(new Daggers());
        if (DestroyOnPickUp)
        {
            Destroy(gameObject);
        }

    }
}
