using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IAttackable
{
    public int maxHealth = 100;
    private int m_currentHealth;

    public UnityEvent onDeath;
    public UnityEvent onHurt;
    public UnityEvent onHeal;

    void Awake()
    {
        m_currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (m_currentHealth <= 0) return;

        HeroKnight hero = GetComponent<HeroKnight>();
        if (hero != null)
            amount = Mathf.RoundToInt(amount * hero.GetDamageMultiplier());

        m_currentHealth -= amount;
        onHurt.Invoke();

        if (m_currentHealth <= 0)
            onDeath.Invoke();
    }
    

    public void Heal(int amount)
    {
        m_currentHealth = Mathf.Min(m_currentHealth + amount, maxHealth);
        onHeal.Invoke();
    }

    public int GetHealth() => m_currentHealth;
    public float GetHealthPercent() => (float)m_currentHealth / maxHealth;
}