using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;
    
    // Referencer til UI panels
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject howToPlayPanel;
    
    // tjek om spiller er pauset
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    private void Update()
    {
        //Lytter efter input (ESC eller P) - pause/unpause spillet
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            // Hvis vi er i en undermenu i pause - Settings eller How to play, så luk den først
            else if ((settingsPanel != null && settingsPanel.activeSelf) ||
                     (howToPlayPanel != null && howToPlayPanel.activeSelf))
            {
                CloseSubPanels();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    //hvilke panels er aktive
    private void ShowPanels(bool showPause, bool showSettings, bool showHotToPlay)
    {
        if (pausePanel != null)
            pausePanel.SetActive(showPause);
        if (settingsPanel != null)
            settingsPanel.SetActive(showSettings);
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(showHotToPlay);
    }
    
    
    public void PauseGame()
    {
        isPaused = true;
        
        Time.timeScale = 0;
        
        ShowPanels(true, false, false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        ShowPanels(false, false, false);
    }

    public void OpenSettings()
    {
        ShowPanels(false, true, false);
    }

    public void OpenHowToPlay()
    {
        ShowPanels(false, false, true);
    }

    public void CloseSubPanels()
    {
        ShowPanels(true, false, false);
    }

    public void GoToMainMenu()
    {
        //Spillet kører videre - ikke i pause i næste scene
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        
        Application.Quit();
    }
}