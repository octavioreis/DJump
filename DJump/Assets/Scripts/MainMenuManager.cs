using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Start()
    {
        SaveManager.Instance.Load();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(Consts.LevelSelectorSceneName);
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene(Consts.HighScoresSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
