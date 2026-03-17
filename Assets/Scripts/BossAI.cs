using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float speed = 2f;
    public float attackDistance = 2f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackDistance)
        {
            MoveToPlayer();
        }
        else
        {
            Attack();
        }
    }

    void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    void Attack()
    {
        Debug.Log("Boss attacking!");
    }
}