using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;
    void Start()
    {
        GetComponent<Health>().onDeath.AddListener(OnDeath);
    }

    void OnDeath()
    {
        GameManager.Instance.AddScore(scoreValue);
    }
}
