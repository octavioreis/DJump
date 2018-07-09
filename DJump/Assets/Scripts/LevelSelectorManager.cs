using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    public Button Level1Button;
    public Button Level2Button;
    public Button Level3Button;
    public InputField PlayerNameInputField;

    private bool _isFreeRun;
    private readonly string _placeholderPlayerName = "ABC";

    public void Start()
    {
        bool level2Enabled;
        bool level3Enabled;

        if (!bool.TryParse(PlayerPrefs.GetString(Consts.Level2Enabled), out level2Enabled))
            level2Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(Consts.Level3Enabled), out level3Enabled))
            level3Enabled = true;

        if (!bool.TryParse(PlayerPrefs.GetString(Consts.FreeRunEnabled), out _isFreeRun))
            _isFreeRun = true;

        if (level2Enabled)
            Level2Button.interactable = Level2Button.enabled = true;

        if (level3Enabled)
            Level3Button.interactable = Level3Button.enabled = true;

        if (!_isFreeRun)
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
        PlayerPrefs.SetString(Consts.FreeRun, _isFreeRun.ToString());
        PlayerPrefs.SetString(Consts.PlayerName, GetPlayerName());
    }

    private string GetPlayerName()
    {
        if (!_isFreeRun)
            return _placeholderPlayerName;

        var playerName = PlayerNameInputField.text;
        if (playerName.IsNullOrWhiteSpace())
            playerName = _placeholderPlayerName;

        return playerName;
    }
}