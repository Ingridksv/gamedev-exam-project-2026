
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public AudioClip gameOverSound;
    private AudioSource m_audioSource;
    public bool gameEnded = false;

    void Start()
    {
        Debug.Log("GameManager is active");
        
        m_audioSource = GetComponent<AudioSource>();

        UIManager.Instance.UpdateScore(score);
    }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: "+ score);
        
        UIManager.Instance.UpdateScore(score);
    }

    public void GameOver()
    {
        if (gameEnded) return;
        
        Debug.Log("GAME OVER called");

        gameEnded = true;
        m_audioSource.PlayOneShot(gameOverSound);
        Time.timeScale = 0f;
        
        UIManager.Instance.ShowGameOver();
        Debug.Log("GAME OVER");
    }

    public void WinGame()
    {
        if (gameEnded) return;
        
        gameEnded = true;
        Time.timeScale = 0f;
        
        UIManager.Instance.ShowWin();
        Debug.Log("YOU WIN");
    }

}
