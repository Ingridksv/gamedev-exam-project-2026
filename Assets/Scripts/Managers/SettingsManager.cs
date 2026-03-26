using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public bool showHP = true;
    public bool soundOn = true;
    public float volume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ApplyAudio();
    }

    public void ApplyAudio()
    {
        AudioListener.volume = soundOn ? volume : 0f;
    }
}