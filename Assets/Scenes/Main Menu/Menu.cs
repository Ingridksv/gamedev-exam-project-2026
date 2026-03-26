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
        Debug.Log("Exit");
        Application.Quit();
    }
}
