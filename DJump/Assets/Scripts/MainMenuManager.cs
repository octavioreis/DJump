using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string LevelSelectorSceneName;

    public void Start()
    {
        SaveManager.Instance.Load();

        PlayerPrefs.SetString(Keys.Level1Enabled, SaveManager.Instance.Level1Enabled.ToString());
        PlayerPrefs.SetString(Keys.Level2Enabled, SaveManager.Instance.Level2Enabled.ToString());
        PlayerPrefs.SetString(Keys.Level3Enabled, SaveManager.Instance.Level3Enabled.ToString());
        PlayerPrefs.SetString(Keys.FreeRunEnabled, SaveManager.Instance.FreeRunEnabled.ToString());
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(LevelSelectorSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
