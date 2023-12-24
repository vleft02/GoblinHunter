using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingInteractable : Interactable
{
    private PlayerUI _playerUI;
    private PlayerController _playerController;
    [SerializeField] int healingAmount = 10;
    [SerializeField] bool destroyOnPickup;

    private void Start()
    {
        _playerUI = GameObject.Find("Player").GetComponent<PlayerUI>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public override string GetPromptMessage()
    {
        return "Press E to consume";
    }

    public override void Interact()
    {
        _playerUI.PlayHealthVFX();
        _playerController.Heal(healingAmount);
        if (destroyOnPickup) 
        {
            Destroy(gameObject);
        }
    }

}
