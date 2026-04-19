using UnityEngine;
using System.Collections.Generic;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 10;
    public string targetLayer = "Enemy"; // set to "Player" on enemy hitboxes
    
    private bool m_isActive = false;
    private HashSet<Transform> m_alreadyHit = new HashSet<Transform>();
    private Collider2D m_collider;

    void Awake()
    {
        m_collider = GetComponent<Collider2D>();
        m_collider.enabled = false;
    }
    public void EnableHitbox()
    {
        m_isActive = true;
        m_alreadyHit.Clear();
        m_collider.enabled = true;
        Debug.Log($"Hitbox enabled with damage: {damage}");
    }
    public void DisableHitbox()
    {
        m_isActive = false;
        m_alreadyHit.Clear();
        m_collider.enabled = false;
        Debug.Log("Hitbox disabled");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_isActive) return;
        ProcessHit(other);
    }

    private void ProcessHit(Collider2D other)
    {
        Transform root = other.transform.root;

        if (root == transform.root) return; 
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        if (root.gameObject.layer != LayerMask.NameToLayer(targetLayer)) return;
        if (m_alreadyHit.Contains(root)) return;
        
        m_alreadyHit.Add(root);


        IAttackable target = root.GetComponent<IAttackable>();
        if (target != null)
        {
            // Debug.Log("Hitbox hit: " + root.name);
            Debug.Log("Hit: " + root.name);
            int finalDamage = damage;

            if (targetLayer == "Enemy")
            {
                finalDamage = Mathf.RoundToInt(damage * GameSettings.Instance.playerDamageMultiplier);
            }
            else if (targetLayer == "Player")
            {
                finalDamage = Mathf.RoundToInt(damage * GameSettings.Instance.enemyDamageMultiplier);
            }

            target.TakeDamage(finalDamage);
        }
    }
}