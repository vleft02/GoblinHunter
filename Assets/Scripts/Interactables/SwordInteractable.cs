using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractable : Interactable
{
    /*    private void Start()
        {
            EventManager += 
        }*/
    [SerializeField] bool DestroyOnPickUp;

    public override string GetPromptMessage()
    {
        return "Pick Up Sword";
    }

    public override void Interact()
    {
        WeaponManager.ChangeWeapon(new Sword());
        if (DestroyOnPickUp) 
        {
            Destroy(gameObject);
        }
        
    }
}
