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
    [SerializeField] private Image damageRadialFX;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject Parent;

    private void Awake()
    {
        healthRadialFX.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.PlayerDeathEvent += ShowDeathScreen;
        EventManager.PlayerHitEvent += ShowDamageAnimation;
    }
    private void Start()
    {
        StopHealthAnimation();
        healthBar.value = 100;
        staminaBar.value = 50;
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
    private void DisableRadialVfx()
    {
        UnityEngine.Color color = damageRadialFX.color;
        color.a = 0f;
        damageRadialFX.color = color;
        damageRadialFX.enabled = false;
    }

    private void ShowDamageAnimation()
    {
        damageRadialFX.enabled = true;
        UnityEngine.Color color = damageRadialFX.color;
        color.a = 1f;
        damageRadialFX.color = color;
        Invoke("DisableRadialVfx", 2f);
    }

    public void ShowDeathScreen() 
    {
       GameObject deathScreenObject = Instantiate(deathScreen);
       deathScreenObject.transform.SetParent(Parent.transform);
       deathScreenObject.transform.localPosition = new Vector3(0, 0, 0);
    }

}
