using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _crosshair;
    [SerializeField] private TextMeshProUGUI _notifText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider staminaBar;

    private void Awake()
    {

    }

    private void Start()
    {

        healthBar.value = 1;
        staminaBar.value = 1;
    }

    void UpdateHealth() 
    {
    }

    void Update()
    {
        /*healthBar.value = */
        staminaBar.value = (GetComponent<PlayerController>().stamina) * 0.1f;
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

}
