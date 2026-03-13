using UnityEngine;
using System.Collections.Generic;

public class AttackHitbox : MonoBehaviour
{
    public int damage = 10;
    private bool m_isActive = false;
    private HashSet<Transform> m_alreadyHit = new HashSet<Transform>();
    private Collider2D m_collider;
    public string targetLayer = "Enemy"; // set to "Player" on enemy hitboxes
    public void EnableHitbox()
    {
        m_isActive = true;
        m_alreadyHit.Clear();
        m_collider.enabled = false;
        m_collider.enabled = true;
    }
    void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    public void DisableHitbox()
    {
        m_isActive = false;
        m_alreadyHit.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ProcessHit(other);
    }

    // Catches the case where player is already inside when hitbox activates
    private void OnTriggerStay2D(Collider2D other)
    {
        ProcessHit(other);
    }

    private void ProcessHit(Collider2D other)
    {
        if (!m_isActive) return;

        Transform root = other.transform.root;

        if (root == transform.root) return;
        if (root.gameObject.layer != LayerMask.NameToLayer(targetLayer)) return;
        if (m_alreadyHit.Contains(root)) return;
        m_alreadyHit.Add(root);

        // Debug.Log("Hitbox hit: " + root.name);
        Debug.Log("Hit: " + root.name + " on layer: " + root.gameObject.layer);
        IAttackable target = root.GetComponent<IAttackable>();
        if (target != null)
            target.TakeDamage(damage);
        else
            Debug.Log(root.name + " has no IAttackable");
    }
}