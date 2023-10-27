using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _crosshair;
    [SerializeField] private TextMeshProUGUI _notifText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private VisualEffect healthVFX;
    [SerializeField] private VisualEffect healthVFX_0;
    [SerializeField] private Image healthRadialFX;

    private void Awake()
    {
        healthRadialFX.enabled = false;
    }

    private void Start()
    {
        
        StopHealthAnimation();

        healthBar.value = 100;
        staminaBar.value = 50;
    }

    void UpdateHealth() 
    {
    }

    void Update()
    {

        healthBar.value = GetComponent<PlayerController>().health;
        staminaBar.value = GetComponent<PlayerController>().stamina;

    }

    public void updateText(string text)
    {
        _notifText.text = text;
    }

    public void crosshairInteraction()
    {
        _crosshair.color = Color.red;
    }

    public void crosshairNoInteraction()
    {
        _crosshair.color = Color.black;
    }

    public void PlayHealthAnimation()
    {
        healthRadialFX.enabled = true;
        //healthVFX.Play();
        //healthVFX_0.Play();
    }

    public void StopHealthAnimation()
    {
        healthRadialFX.enabled = false;
        healthVFX.Stop();
        healthVFX_0.Stop();
    }


}
