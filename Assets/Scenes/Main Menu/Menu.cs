using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level_uno");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
