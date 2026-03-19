using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] public float m_detectionRange = 6f;
    [SerializeField] public float m_attackRange = 1.2f;
    [SerializeField] public float m_moveSpeed = 2.5f;
    [SerializeField] public float m_attackCooldown = 1.5f;

    public Transform m_player;
    public float m_timeSinceAttack = 0f;
    public bool m_isDead = false;

    // States other scripts can read
    Vector3 Center => transform.position + Vector3.up * 0.7f;

    public bool InAttackRange => Vector2.Distance(Center, m_player.position) <= m_attackRange;
    public bool InDetectionRange => Vector2.Distance(Center, m_player.position) <= m_detectionRange;
    public bool AttackReady => m_timeSinceAttack >= m_attackCooldown;

    void Start()
    {
        m_player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (m_isDead) return;
        m_timeSinceAttack += Time.deltaTime;
    }
    
    // void OnDrawGizmosSelected()
    // {
    //     // Offset up to the center of the sprite
    //
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(Center, m_attackRange);
    //
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(Center, m_detectionRange);
    // }

    public void FacePlayer(bool invertScale = false)
    {
        float dir = m_player.position.x > transform.position.x ? 1f : -1f;
        if (invertScale) dir = -dir;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    public Vector2 DirectionToPlayer()
    {
        return (m_player.position - transform.position).normalized;
    }

    public void ResetAttackTimer()
    {
        m_timeSinceAttack = 0f;
    }
}