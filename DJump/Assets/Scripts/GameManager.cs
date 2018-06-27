using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int GameScore;
    public static int GameLives;
    public static float HalfScreenHeight = 4.6f;
    public static float HorizontalMaxLimit = 3.5f;
    public static float HorizontalMinLimit = -3.5f;

    public int DistanceNeededToFinishLevel1 = 250;
    public int DistanceNeededToFinishLevel2 = 350;
    public int DistanceNeededToFinishLevel3 = 450;
    public int StartingLives = 1;
    public string EndGameSceneName;

    public Text NeededScoreText;
    public Text ScoreText;

    private Levels _currentLevel;
    private bool _isFreeRun;
    private int _scoreNeeded;

    public void Start()
    {
        if (!bool.TryParse(PlayerPrefs.GetString(Consts.FreeRun), out _isFreeRun))
            _isFreeRun = true;

        _currentLevel = (Levels)Enum.Parse(typeof(Levels), PlayerPrefs.GetString(Consts.CurrentLevel));

        switch (_currentLevel)
        {
            case Levels.Level1:
                _scoreNeeded = DistanceNeededToFinishLevel1;
                break;
            case Levels.Level2:
                _scoreNeeded = DistanceNeededToFinishLevel2;
                break;
            case Levels.Level3:
                _scoreNeeded = DistanceNeededToFinishLevel3;
                break;
        }

        GameLives = StartingLives;
        GameScore = 0;
    }

    public void Update()
    {
        ScoreText.text = string.Concat("Score: ", GameScore);

        if (!_isFreeRun)
        {
            NeededScoreText.text = string.Concat("Goal Score: ", _scoreNeeded);

            if (GameScore >= _scoreNeeded)
                EndGame();
        }
        else
        {
            NeededScoreText.enabled = false;
        }

        if (GameLives == 0)
            EndGame(true);
    }

    private void EndGame(bool playerDied = false)
    {
        PlayerPrefs.SetString(Consts.PlayerDied, playerDied.ToString());
        PlayerPrefs.SetInt(Consts.Score, GameScore);
        SceneManager.LoadScene(EndGameSceneName);
    }
}