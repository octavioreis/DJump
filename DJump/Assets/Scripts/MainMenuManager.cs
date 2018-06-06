using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string LevelSelectorSceneName;

    public void StartNewGame()
    {
        SceneManager.LoadScene(LevelSelectorSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
