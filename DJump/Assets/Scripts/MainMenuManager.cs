using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static readonly string CurrentLevelKey = "CurrentLevel";
    public static readonly string Level1EnabledKey = "Level1Enabled";
    public static readonly string Level2EnabledKey = "Level2Enabled";
    public static readonly string Level3EnabledKey = "Level3Enabled";
    public static readonly string FreeRunKey = "FreeRun";
    public static readonly string FreeRunEnabledKey = "FreeRunEnabled";
    public static readonly string ScoreKey = "Score";

    public string LevelSelectorSceneName;

    private bool _level1Enabled = false;
    private bool _level2Enabled = false;
    private bool _level3Enabled = false;
    private bool _freeRunEnabled = false;

    public void Start()
    {
        SaveManager.LoadSave(out _level1Enabled, out _level2Enabled, out _level3Enabled, out _freeRunEnabled);

        PlayerPrefs.SetString(Level1EnabledKey, _level1Enabled.ToString());
        PlayerPrefs.SetString(Level2EnabledKey, _level2Enabled.ToString());
        PlayerPrefs.SetString(Level3EnabledKey, _level3Enabled.ToString());
        PlayerPrefs.SetString(FreeRunEnabledKey, _freeRunEnabled.ToString());
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
