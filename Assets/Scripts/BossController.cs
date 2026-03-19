using UnityEngine;

public class BossController : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Health m_health;
    private AIController m_ai;
  
    void Start()
    {
        
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_ai = GetComponent<AIController>();

        m_health = GetComponent<Health>();
        m_health.onHurt.AddListener(OnHurt);
        m_health.onDeath.AddListener(OnDeath);
       
    }

    void Update()
    {
        
        if (m_ai.m_isDead) return;
       // Debug.Log($"DetectionRange: {m_ai.InDetectionRange}, AttackRange: {m_ai.InAttackRange}, AttackReady: {m_ai.AttackReady}");
        m_ai.FacePlayer(invertScale: true);

        if (m_ai.InAttackRange && m_ai.AttackReady)
        {
            Attack();
        }
        else if (m_ai.InDetectionRange && !m_ai.InAttackRange)
        {
            Chase();
        }
        else if (m_ai.InAttackRange)
        {
            m_body2d.linearVelocity = Vector2.zero;
            m_animator.SetInteger("AnimState", 0);
        }
        else
        {
            m_body2d.linearVelocity = Vector2.zero;
            m_animator.SetInteger("AnimState", 0);
        }
    }

    void Chase()
    {
        m_animator.SetInteger("AnimState", 1);
        m_body2d.linearVelocity = m_ai.DirectionToPlayer() * m_ai.m_moveSpeed;
    }

    void Attack()
    {
        m_ai.ResetAttackTimer();
        m_body2d.linearVelocity = Vector2.zero;
        m_animator.SetTrigger("Attack");
    }

    void EnableHitbox()  => GetComponentInChildren<AttackHitbox>()?.EnableHitbox();
    void DisableHitbox() => GetComponentInChildren<AttackHitbox>()?.DisableHitbox();

    void OnHurt()
    {
        if (!m_ai.m_isDead)
            m_animator.SetTrigger("Hurt");
        
    }

    void OnDeath()
    {
        m_ai.m_isDead = true;
        m_body2d.linearVelocity = Vector2.zero;
        m_animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.5f);
    }
}