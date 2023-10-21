using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    /*
    public void BaseInteract()
    {
        Interact();
    }
    */
    public abstract void Interact();

    public abstract string GetPromptMessage();
    
}
