using UnityEngine;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] float deathDelay = 1.5f;
    bool hasDied = false;
    void Start()
    {
        GetComponent<Health>().onDeath.AddListener(OnDeath);
    }

    void OnDeath()
    {
        if (hasDied) return;
        hasDied =  true;
        
        Debug.Log("Player is dead");
        StartCoroutine(HandleDeath());
    }
    
    IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(deathDelay);

        GameManager.Instance.GameOver();
    }
    
}
