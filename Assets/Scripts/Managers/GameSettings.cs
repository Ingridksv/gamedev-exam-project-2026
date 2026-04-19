using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public string currentDifficulty = "Normal";
    public static GameSettings Instance;

    public float playerDamageMultiplier = 1f;
    public float enemyDamageMultiplier = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            SetNormal();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEasy()
    {
        playerDamageMultiplier = 1.5f;
        enemyDamageMultiplier = 0.75f;
        currentDifficulty = "Easy";
        Debug.Log("SetEasy Activated");
    }
    public void SetNormal()
    {
        playerDamageMultiplier = 1f;
        enemyDamageMultiplier = 1f;
        currentDifficulty = "Normal";
        Debug.Log("SetNormal Activated");
    }

    public void SetHard()
    {
        playerDamageMultiplier = 0.8f;
        enemyDamageMultiplier = 1f; 
        //enemyDamageMultiplier = 1.25f;
        currentDifficulty = "Hard";
        Debug.Log("SetHard Activated");
    }
}
