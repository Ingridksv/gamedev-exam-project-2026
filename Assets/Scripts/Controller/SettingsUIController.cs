using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    public Toggle hpToggle;
    public Toggle soundToggle;
    public Slider volumeSlider;

    private void Start()
    {
        if (SettingsManager.Instance == null)
        {
            Debug.LogError("SettingsManager.Instance is NULL");
            return;
        }

        if (hpToggle != null)
        {
            hpToggle.isOn = SettingsManager.Instance.showHP;
        }
        else
        {
            Debug.LogError("hpToggle is NULL");
        }

        if (soundToggle != null)
        {
            soundToggle.isOn = SettingsManager.Instance.soundOn;
        }
        else
        {
            Debug.LogError("soundToggle is NULL");
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = SettingsManager.Instance.volume;
        }
        else
        {
            Debug.LogError("volumeSlider is NULL");
        }

        SettingsManager.Instance.ApplyAudio();
    }

    public void ToggleHP(bool value)
    {
        if (SettingsManager.Instance != null)
            SettingsManager.Instance.showHP = value;
    }

    public void ToggleSound(bool value)
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.soundOn = value;
            SettingsManager.Instance.ApplyAudio();
        }
    }

    public void SetVolume(float value)
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.volume = value;
            SettingsManager.Instance.ApplyAudio();
        }
    }
}