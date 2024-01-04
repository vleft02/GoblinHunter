using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    public bool triggerEvent;

    public abstract void Interact();

    public abstract string GetPromptMessage();
    
}
