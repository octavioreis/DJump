using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text ContentUnlockedText;
    public Text ScoreText;
    public Text TitleText;
    public string MainMenuSceneName;

    private Levels _currentLevel;
    private bool _isFreeRun;
    private int _playerScore;
    private bool _playerDied;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void Start()
    {
        if (!bool.TryParse(PlayerPrefs.GetString(Keys.FreeRun), out _isFreeRun))
            _isFreeRun = true;

        if (!bool.TryParse(PlayerPrefs.GetString(Keys.PlayerDied), out _playerDied))
            _playerDied = true;

        _currentLevel = (Levels)Enum.Parse(typeof(Levels), PlayerPrefs.GetString(Keys.CurrentLevel));
        _playerScore = PlayerPrefs.GetInt(Keys.Score);

        UpdateSave();

        ContentUnlockedText.enabled = false;
    }

    public void Update()
    {
        ScoreText.text = string.Concat("Final Score: ", _playerScore);

        if (!_isFreeRun && !_playerDied)
        {
            TitleText.text = "LEVEL COMPLETED";

            if (_currentLevel == Levels.Level1)
            {
                ContentUnlockedText.text = "You unlocked the Sky stage!";
                ContentUnlockedText.enabled = true;
            }
            else if (_currentLevel == Levels.Level2)
            {
                ContentUnlockedText.text = "You unlocked the Space stage!";
                ContentUnlockedText.enabled = true;
            }
            else if (_currentLevel == Levels.Level3)
            {
                ContentUnlockedText.text = "You beat all stages!\nFrom now on all games are in infinite mode!";
                ContentUnlockedText.enabled = true;
            }
        }
        else
        {
            TitleText.text = "GAME OVER";
        }
    }

    private void UpdateSave()
    {
        SaveManager.Instance.Level1Enabled = true;

        if (!_isFreeRun)
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
                        SaveManager.Instance.FreeRunEnabled = true;
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            //save score
        }

        SaveManager.Instance.Save();
    }
}
