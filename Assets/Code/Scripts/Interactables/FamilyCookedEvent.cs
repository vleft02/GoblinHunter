using UnityEngine;

public class FamilyCookedEvent : Interactable
{
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

    public void RotateToPot()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRotate>().RotateCameraGameEvent(0, 57.2f, 0, seconds: 0.5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().CameraTransition(4);

    }
}
