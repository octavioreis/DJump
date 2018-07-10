using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    public Button Level2Button;
    public Button Level3Button;
    public InputField PlayerNameInputField;

    private readonly string _placeholderPlayerName = "ABC";

    public void Start()
    {
        if (SaveManager.Instance.Level2Enabled)
            Level2Button.interactable = Level2Button.enabled = true;

        if (SaveManager.Instance.Level3Enabled)
            Level3Button.interactable = Level3Button.enabled = true;

        if (!SaveManager.Instance.StoryModeCompleted)
            PlayerNameInputField.gameObject.SetActive(false);

        var playerName = PlayerPrefs.GetString(Consts.PlayerName);
        if (!playerName.IsNullOrWhiteSpace() && !playerName.Equals(_placeholderPlayerName))
            PlayerNameInputField.text = playerName;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Consts.MainMenuSceneName);
    }

    public void StartLevel1()
    {
        SetPlayerPrefs(Levels.Level1);

        SceneManager.LoadScene(Consts.Level1SceneName);
    }

    public void StartLevel2()
    {
        SetPlayerPrefs(Levels.Level2);

        SceneManager.LoadScene(Consts.Level2SceneName);
    }

    public void StartLevel3()
    {
        SetPlayerPrefs(Levels.Level3);

        SceneManager.LoadScene(Consts.Level3SceneName);
    }

    private void SetPlayerPrefs(Levels selectedLevel)
    {
        PlayerPrefs.SetString(Consts.CurrentLevel, selectedLevel.ToString());
        PlayerPrefs.SetString(Consts.PlayerName, GetPlayerName());
    }

    private string GetPlayerName()
    {
        if (!SaveManager.Instance.StoryModeCompleted)
            return _placeholderPlayerName;

        var playerName = PlayerNameInputField.text;
        if (playerName.IsNullOrWhiteSpace())
            playerName = _placeholderPlayerName;

        return playerName;
    }
}