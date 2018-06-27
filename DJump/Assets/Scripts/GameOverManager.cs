﻿using System;
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
    private bool _playerDied;
    private string _playerName;
    private int _playerScore;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void Start()
    {
        if (!bool.TryParse(PlayerPrefs.GetString(Consts.FreeRun), out _isFreeRun))
            _isFreeRun = true;

        if (!bool.TryParse(PlayerPrefs.GetString(Consts.PlayerDied), out _playerDied))
            _playerDied = true;

        _currentLevel = (Levels)Enum.Parse(typeof(Levels), PlayerPrefs.GetString(Consts.CurrentLevel));
        _playerName = PlayerPrefs.GetString(Consts.PlayerName);
        _playerScore = PlayerPrefs.GetInt(Consts.Score);

        UpdateSave();

        ContentUnlockedText.enabled = false;
    }

    public void Update()
    {
        ScoreText.text = string.Concat("Final Score: ", _playerScore);

        if (!_isFreeRun && !_playerDied)
        {
            TitleText.text = "LEVEL COMPLETED";

            switch (_currentLevel)
            {
                case Levels.Level1:
                    ContentUnlockedText.text = "You unlocked the Sky stage!";
                    ContentUnlockedText.enabled = true;
                    break;
                case Levels.Level2:
                    ContentUnlockedText.text = "You unlocked the Space stage!";
                    ContentUnlockedText.enabled = true;
                    break;
                case Levels.Level3:
                    ContentUnlockedText.text = "You beat all stages!\nFrom now on all games are in infinite mode!";
                    ContentUnlockedText.enabled = true;
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
