using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    public bool triggerEvent;

    public abstract void Interact();

    public abstract string GetPromptMessage();
    
}
