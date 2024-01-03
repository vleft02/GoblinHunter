using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject equipMenu;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject introBackground;
    [SerializeField] private GameObject introText;
    [SerializeField] private GameObject introTextStudio;
    [SerializeField] private GameObject introTextPresents;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        EventManager.PlayerDeathEvent += DeathUI;
        EventManager.PlayerHitEffectEvent += PlayDamageVfx;
        EventManager.TogglePause += TogglePauseMenu;
        EventManager.ToggleEquipMenu += ToggleRadialMenu;
        EventManager.BossDefeatedEvent += FadeToBlack;
    }
    private void OnDisable()
    {
        EventManager.PlayerDeathEvent -= DeathUI;
        EventManager.PlayerHitEffectEvent -= PlayDamageVfx;
        EventManager.TogglePause -= TogglePauseMenu;
        EventManager.ToggleEquipMenu -= ToggleRadialMenu;
        EventManager.ToggleEquipMenu -= FadeToBlack;
    }

    private void ToggleRadialMenu()
    {
        if (equipMenu.activeSelf)
        {
            equipMenu.SetActive(false);
            _crosshair.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else
        {
            equipMenu.SetActive(true);
            _crosshair.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }

    private void Start()
    {
        /*StopHealthAnimation();*/
        healthBar.value = 100;
        staminaBar.value = 50;
    }

    void Update()
    {

        healthBar.value = GetComponent<PlayerController>().player.health;
        staminaBar.value = GetComponent<PlayerController>().player.stamina;

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

    public void PlayHealthVFX()
    {
        StartCoroutine(Effects.StartFade(healthRadialFX, 0.5f, 1f));
        Invoke("DisableHealthlVfx", 1f);
    }

    public void DisableHealthlVfx()
    {
        StartCoroutine(Effects.StartFade(healthRadialFX, 0.5f, 0f));
        healthVFX.Stop();
        healthVFX_0.Stop();
    }
    private void DisableDamageVfx()
    {
        StartCoroutine(Effects.StartFade(damageRadialFX, 0.5f, 0f));
    }


    private void PlayDamageVfx()
    {
        StartCoroutine(Effects.StartFade(damageRadialFX, 0.5f, 1f));
        Invoke("DisableDamageVfx", 1f);
    }

    public void DeathUI()
    {
        _crosshair.enabled = false;
        Invoke("ShowDeathScreen", 1.5f);
    }

    public void ShowDeathScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void FadeToBlack()
    {
        blackScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(Effects.StartFadeWithFunction(blackScreen.GetComponent<Image>(), 5f, 1f, RollCredits));
        StartCoroutine(Effects.StartFade(GameObject.Find("GameManager")?.GetComponent<AudioSource>(), 5f, 0f));
    }

    private void RollCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    private void TogglePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            healthBar.enabled = true;
            staminaBar.enabled = true;
            _crosshair.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else
        {
            pauseMenu.SetActive(true);
            healthBar.enabled = false;
            staminaBar.enabled = false;
            _crosshair.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }

    }

    public void ShowIntroBackground()
    {
        introBackground.SetActive(true);
    }

    public void HideIntroBackground()
    {
        introBackground.SetActive(false);
    }

    public void ShowIntroText()
    {
        introText.SetActive(true);
    }

    public void HideIntroText()
    {
        introText.SetActive(false);
    }

    public void ShowIntroTextStudio()
    {
        introTextStudio.SetActive(true);
    }

    public void HideIntroTextStudio()
    {
        introTextStudio.SetActive(false);
    }

    public void ShowIntroTextPresents()
    {
        introTextPresents.SetActive(true);
    }

    public void HideIntroTextPresents()
    {
        introTextPresents.SetActive(false);
    }

}
