using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : Interactable
{
    public override string GetPromptMessage()
    {
        return "Pick up key";
    }

    public override void Interact()
    {
        PlayerProfile.gameData.currentArea.SetKey(true);
        Destroy(gameObject);
    }
}
