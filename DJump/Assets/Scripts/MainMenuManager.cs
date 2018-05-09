using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string NewGameSceneName;

    public void StartNewGame()
    {
        SceneManager.LoadScene(NewGameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
