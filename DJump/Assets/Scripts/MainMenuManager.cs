using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Start()
    {
        SaveManager.Instance.Load();

        PlayerPrefs.SetString(Consts.Level2Enabled, SaveManager.Instance.Level2Enabled.ToString());
        PlayerPrefs.SetString(Consts.Level3Enabled, SaveManager.Instance.Level3Enabled.ToString());
        PlayerPrefs.SetString(Consts.StoryMode, (!SaveManager.Instance.StoryModeCompleted).ToString());
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
