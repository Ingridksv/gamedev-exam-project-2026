using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
   // [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] Transform healthBar;
    [SerializeField] float m_attackCooldown = 0.5f;


    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
   // private bool                m_isWallSliding = false;
   // private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    private Health              m_health;
    private AttackHitbox       m_attackHitbox;
    Vector3 healthBarScale;
    private bool  m_isBlocking = false;

    


    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        m_health = GetComponent<Health>();
        m_health.onHurt.AddListener(OnHurt);
        m_health.onDeath.AddListener(OnDeath);
        m_attackHitbox = GetComponentInChildren<AttackHitbox>();
        healthBarScale = healthBar.localScale;
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        
        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;
        

      
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Swap direction of sprite depending on walk direction
        
        if (inputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            m_facingDirection = 1;
            healthBar.localScale = new Vector3(Mathf.Abs(healthBarScale.x), healthBarScale.y, healthBarScale.z);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            m_facingDirection = -1;
            healthBar.localScale = new Vector3(-Mathf.Abs(healthBarScale.x), healthBarScale.y, healthBarScale.z);
        }

        // Move
        if (!m_rolling )
            // m_body2d.linearVelocity = new Vector2(inputX * m_speed, m_body2d.linearVelocity.y);
            m_body2d.linearVelocity = new Vector2(inputX * m_speed, inputY * m_speed);
        
        // -- Handle Animations --

        // //Death
        // if (Input.GetKeyDown("e") && !m_rolling)
        // {
        //     m_health.TakeDamage(999);
        // }
        //     
        // //Hurt
        // else if (Input.GetKeyDown("q") && !m_rolling){
        //     m_health.TakeDamage(10);
        // }

        //Attack
       if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > m_attackCooldown && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            m_isBlocking = true;
        }

        else if (Input.GetMouseButtonUp(1)){
            m_animator.SetBool("IdleBlock", false);
            m_isBlocking = false;
        }

        // // Roll
        // else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        // {
        //     m_rolling = true;
        //     m_animator.SetTrigger("Roll");
        //     m_body2d.linearVelocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.linearVelocity.y);
        // }
        
        // Roll direction
        else if (Input.GetKeyDown("left shift") && !m_rolling)
        {
            m_rolling = true;
            m_rollCurrentTime = 0f; // Add this
            m_animator.SetTrigger("Roll");
            Vector2 rollDir = new Vector2(inputX, inputY).normalized;
            if (rollDir == Vector2.zero) rollDir = new Vector2(m_facingDirection, 0);
            m_body2d.linearVelocity = rollDir * m_rollForce;
        }
        

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon)
        {
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }
    
    void OnHurt()
    {
        m_animator.SetTrigger("Hurt");
    }

    void OnDeath()
    {
        m_animator.SetTrigger("Death");
        m_animator.SetBool("noBlood", m_noBlood);
    
        // Disable player input on death
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        m_body2d.linearVelocity = Vector2.zero;
    }
    
    void EnableHitbox()
    {
        m_attackHitbox.EnableHitbox();
    }

    void DisableHitbox()
    {
        m_attackHitbox.DisableHitbox();
    }
    
    public float GetDamageMultiplier()
    {
        return m_isBlocking ? 0.25f : 1.0f;
    }
}
