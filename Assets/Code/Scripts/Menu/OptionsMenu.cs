using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Toggle GoreToggle;
    [SerializeField] Toggle FullScreenToggle;
    [SerializeField] Slider VolumeSlider;

    public void OnEnable()
    {
        GoreToggle.isOn = PlayerProfile.GetGoreEnabled();
        FullScreenToggle.isOn = Screen.fullScreen;
        VolumeSlider.value = PlayerProfile.GetVolume();
        
        GoreToggle.onValueChanged.AddListener(ToggleGore);
        FullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);
        VolumeSlider.onValueChanged.AddListener(VolumeChange);
    }

    private void OnDisable()
    {
        GoreToggle.onValueChanged.RemoveListener(ToggleGore);
        FullScreenToggle.onValueChanged.RemoveListener(ToggleFullScreen);
        VolumeSlider.onValueChanged.RemoveListener(VolumeChange);
    }
    public void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void ToggleGore(bool value)
    {
        PlayerProfile.SetGoreEnabled(value);
    }


    public void VolumeChange(float value) 
    { 
        PlayerProfile.SetVolume(value);
    }
}
