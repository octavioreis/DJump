using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public readonly static float HalfScreenHeight = 4.6f;
    public readonly static float PlatformHorizontalMaxLimit = 3.5f;
    public readonly static float PlatformHorizontalMinLimit = -3.5f;
    public readonly static float EnemyHorizontalMaxLimit = 3f;
    public readonly static float EnemyHorizontalMinLimit = -3f;
    public readonly static float PlayerHorizontalMaxLimit = 4f;
    public readonly static float PlayerHorizontalMinLimit = -4f;

    public static int GameScore;
    public static GameOverReason? EndingGameReason;

    public int DistanceNeededToFinishLevel1 = 200;
    public int DistanceNeededToFinishLevel2 = 275;
    public int DistanceNeededToFinishLevel3 = 350;
    public int StartingLives = 1;

    public Text NeededScoreText;
    public Text ScoreText;

    private Levels _currentLevel;
    private int _scoreNeeded;

    public void Start()
    {
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

        EndingGameReason = null;
        GameScore = 0;
    }

    public void Update()
    {
        ScoreText.text = string.Concat("Score: ", GameScore);

        if (!SaveManager.Instance.StoryModeCompleted)
        {
            NeededScoreText.text = string.Concat("Goal Score: ", _scoreNeeded);

            if (GameScore >= _scoreNeeded)
                EndGame(GameOverReason.LevelCompleted);
        }
        else
        {
            NeededScoreText.enabled = false;
        }

        if (EndingGameReason != null)
            EndGame(EndingGameReason.Value);
    }

    private void EndGame(GameOverReason cause)
    {
        var playerDied = cause != GameOverReason.LevelCompleted && cause != GameOverReason.Quit;

        PlayerPrefs.SetString(Consts.PlayerDied, playerDied.ToString());
        PlayerPrefs.SetInt(Consts.Score, GameScore);
        SceneManager.LoadScene(Consts.GameOverSceneName);
    }
}