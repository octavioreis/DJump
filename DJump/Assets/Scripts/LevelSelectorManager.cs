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
    public Toggle FreeRunToggle;

    private bool _isFreeRun;

    public void Start()
    {
        bool level1Enabled;
        bool level2Enabled;
        bool level3Enabled;
        bool freeRunEnabled;

        if (!bool.TryParse(PlayerPrefs.GetString(MainMenuManager.Level1EnabledKey), out level1Enabled))
            level1Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(MainMenuManager.Level2EnabledKey), out level2Enabled))
            level2Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(MainMenuManager.Level3EnabledKey), out level3Enabled))
            level3Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(MainMenuManager.FreeRunEnabledKey), out freeRunEnabled))
            freeRunEnabled = true;

        if (level1Enabled)
            Level1Button.interactable = Level1Button.enabled = true;

        if (level2Enabled)
            Level2Button.interactable = Level2Button.enabled = true;

        if (level3Enabled)
            Level3Button.interactable = Level3Button.enabled = true;

        if (freeRunEnabled)
            FreeRunToggle.interactable = FreeRunToggle.enabled = true;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void StartLevel1()
    {
        PlayerPrefs.SetString(MainMenuManager.CurrentLevelKey, Levels.Level1.ToString());
        PlayerPrefs.SetString(MainMenuManager.FreeRunKey, _isFreeRun.ToString());

        SceneManager.LoadScene(Level1SceneName);
    }

    public void StartLevel2()
    {
        PlayerPrefs.SetString(MainMenuManager.CurrentLevelKey, Levels.Level2.ToString());
        PlayerPrefs.SetString(MainMenuManager.FreeRunKey, _isFreeRun.ToString());

        SceneManager.LoadScene(Level2SceneName);
    }

    public void StartLevel3()
    {
        PlayerPrefs.SetString(MainMenuManager.CurrentLevelKey, Levels.Level3.ToString());
        PlayerPrefs.SetString(MainMenuManager.FreeRunKey, _isFreeRun.ToString());

        SceneManager.LoadScene(Level3SceneName);
    }

    public void FreeRunCheckedChanged()
    {
        _isFreeRun = FreeRunToggle.isOn;
    }
}