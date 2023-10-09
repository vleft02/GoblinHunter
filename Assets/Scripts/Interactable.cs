using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable 
{
    /*
    public void BaseInteract()
    {
        Interact();
    }
    */
    public void Interact();

    public string GetPromptMessage();
    
}
