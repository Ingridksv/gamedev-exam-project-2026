using UnityEngine;

public class HealthUIBinder : MonoBehaviour
{
    public Health health;

    private void Start()
    {
        health.onHurt.AddListener(UpdateUIText);
        health.onHeal.AddListener(UpdateUIText);
        health.onDeath.AddListener(UpdateUIText);

        UpdateUIText();
    }

    void UpdateUIText()
    {
        if (UIManager.Instance != null && SettingsManager.Instance != null)
        {
            if (SettingsManager.Instance.showHP)
            {
                UIManager.Instance.UpdateHealth(
                    health.GetHealth(),
                    health.maxHealth
                );
            }
        }
    }
}