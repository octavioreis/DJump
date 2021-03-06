﻿using System;
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

    public Texture2D MouseTexture;
    public GameObject FinishLine;
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
            default:
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

        if (!SaveManager.Instance.StoryModeCompleted)
        {
            FinishLine.SetActive(true);
            var position = FinishLine.transform.position;
            position.y = _scoreNeeded;
            FinishLine.transform.position = position;
        }

        Cursor.SetCursor(MouseTexture, new Vector2(MouseTexture.width / 2, MouseTexture.height / 2), CursorMode.Auto);
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
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        PlayerPrefs.SetString(Consts.PlayerDied, (cause == GameOverReason.Death || cause == GameOverReason.Quit).ToString());
        PlayerPrefs.SetInt(Consts.Score, GameScore);
        SceneManager.LoadScene(Consts.GameOverSceneName);
    }
}