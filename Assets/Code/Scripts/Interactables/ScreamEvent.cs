using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreamEvent : Interactable
{

    private void Start()
    {
        
    }

    public override string GetPromptMessage()
    {
        return "";
    }

    public override void Interact()
    {
        if (useEvents)
        {
            if (triggerEvent)
            {
                GetComponent<InteractionEvent>().onInteract.Invoke();
            }


        }
        else
        {
            Debug.Log("Fix: Use Events needs to be active!");
        }
    }

    public void RotateToScream()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRotate>().RotateCameraGameEvent(0f, 45f, 0f, seconds:0.5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CameraTransition(4);

    }

}
