using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string LevelSelectorSceneName;
    public string HighScoresSceneName;

    public void Start()
    {
        SaveManager.Instance.Load();

        PlayerPrefs.SetString(Consts.Level2Enabled, SaveManager.Instance.Level2Enabled.ToString());
        PlayerPrefs.SetString(Consts.Level3Enabled, SaveManager.Instance.Level3Enabled.ToString());
        PlayerPrefs.SetString(Consts.FreeRunEnabled, SaveManager.Instance.FreeRunEnabled.ToString());
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(LevelSelectorSceneName);
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene(HighScoresSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
