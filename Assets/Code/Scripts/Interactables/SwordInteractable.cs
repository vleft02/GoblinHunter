using UnityEngine;

public class SwordInteractable : Interactable
{

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
