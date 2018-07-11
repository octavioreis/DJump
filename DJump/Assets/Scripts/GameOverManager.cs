using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text ContentUnlockedText;
    public Text ScoreText;
    public Text TitleText;

    private Levels _currentLevel;
    private bool _playerDied;
    private string _playerName;
    private int _playerScore;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Consts.MainMenuSceneName);
    }

    public void Start()
    {
        if (!bool.TryParse(PlayerPrefs.GetString(Consts.PlayerDied), out _playerDied))
            _playerDied = true;

        _currentLevel = (Levels)Enum.Parse(typeof(Levels), PlayerPrefs.GetString(Consts.CurrentLevel));
        _playerName = PlayerPrefs.GetString(Consts.PlayerName);
        _playerScore = PlayerPrefs.GetInt(Consts.Score);

        UpdateText();
        UpdateSave();
    }

    private void UpdateText()
    {
        ScoreText.text = string.Concat("Final Score: ", _playerScore);

        if (!SaveManager.Instance.StoryModeCompleted && !_playerDied)
        {
            TitleText.text = "LEVEL COMPLETED";

            switch (_currentLevel)
            {
                case Levels.Level1:
                    if (!SaveManager.Instance.Level2Enabled)
                    {
                        ContentUnlockedText.text = "You unlocked the Sky stage!";
                        ContentUnlockedText.gameObject.SetActive(true);
                    }
                    break;
                case Levels.Level2:
                    if (!SaveManager.Instance.Level3Enabled)
                    {
                        ContentUnlockedText.text = "You unlocked the Space stage!";
                        ContentUnlockedText.gameObject.SetActive(true);
                    }
                    break;
                case Levels.Level3:
                    ContentUnlockedText.text = "You beat all stages!\nFrom now on all games are in infinite mode!";
                    ContentUnlockedText.gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            TitleText.text = "GAME OVER";
        }
    }

    private void UpdateSave()
    {
        if (!SaveManager.Instance.StoryModeCompleted)
        {
            if (!_playerDied)
            {
                switch (_currentLevel)
                {
                    case Levels.Level1:
                        SaveManager.Instance.Level2Enabled = true;
                        break;
                    case Levels.Level2:
                        SaveManager.Instance.Level3Enabled = true;
                        break;
                    case Levels.Level3:
                        SaveManager.Instance.StoryModeCompleted = true;
                        break;
                }
            }
        }
        else
        {
            SaveManager.Instance.PlayerScores.Add(new PlayerScore(_playerName, _playerScore));
        }

        SaveManager.Instance.Save();
    }
}
