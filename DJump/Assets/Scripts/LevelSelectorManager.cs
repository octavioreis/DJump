using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    public string Level1SceneName;
    public string Level2SceneName;
    public string Level3SceneName;
    public string MainMenuSceneName;
    public Button Level1Button;
    public Button Level2Button;
    public Button Level3Button;

    private bool _isFreeRun;

    public void Start()
    {
        bool level1Enabled;
        bool level2Enabled;
        bool level3Enabled;

        if (!bool.TryParse(PlayerPrefs.GetString(Keys.Level1Enabled), out level1Enabled))
            level1Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(Keys.Level2Enabled), out level2Enabled))
            level2Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(Keys.Level3Enabled), out level3Enabled))
            level3Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(Keys.FreeRunEnabled), out _isFreeRun))
            _isFreeRun = true;

        if (level1Enabled)
            Level1Button.interactable = Level1Button.enabled = true;

        if (level2Enabled)
            Level2Button.interactable = Level2Button.enabled = true;

        if (level3Enabled)
            Level3Button.interactable = Level3Button.enabled = true;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void StartLevel1()
    {
        PlayerPrefs.SetString(Keys.CurrentLevel, Levels.Level1.ToString());
        PlayerPrefs.SetString(Keys.FreeRun, _isFreeRun.ToString());

        SceneManager.LoadScene(Level1SceneName);
    }

    public void StartLevel2()
    {
        PlayerPrefs.SetString(Keys.CurrentLevel, Levels.Level2.ToString());
        PlayerPrefs.SetString(Keys.FreeRun, _isFreeRun.ToString());

        SceneManager.LoadScene(Level2SceneName);
    }

    public void StartLevel3()
    {
        PlayerPrefs.SetString(Keys.CurrentLevel, Levels.Level3.ToString());
        PlayerPrefs.SetString(Keys.FreeRun, _isFreeRun.ToString());

        SceneManager.LoadScene(Level3SceneName);
    }
}