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

    public int DistanceNeededToFinishLevel1 = 200;
    public int DistanceNeededToFinishLevel2 = 300;
    public int DistanceNeededToFinishLevel3 = 400;
    public Text ScoreText;
    public int StartingLives = 1;
    public string EndGameSceneName;

    private Levels _currentLevel;
    private bool _isFreeRun;

    public void Start()
    {
        bool isFreeRun;

        if (!bool.TryParse(PlayerPrefs.GetString(Keys.FreeRun), out isFreeRun))
            isFreeRun = true;

        _currentLevel = (Levels)Enum.Parse(typeof(Levels), PlayerPrefs.GetString(Keys.CurrentLevel));
        _isFreeRun = isFreeRun;

        GameLives = StartingLives;
        GameScore = 0;
    }

    public void Update()
    {
        ScoreText.text = string.Concat("Score: ", GameScore);

        if (!_isFreeRun)
        {
            switch (_currentLevel)
            {
                case Levels.Level1:
                    if (GameScore >= DistanceNeededToFinishLevel1)
                        EndGame();
                    break;
                case Levels.Level2:
                    if (GameScore >= DistanceNeededToFinishLevel2)
                        EndGame();
                    break;
                case Levels.Level3:
                    if (GameScore >= DistanceNeededToFinishLevel3)
                        EndGame();
                    break;
                default:
                    break;
            }
        }

        if (GameLives == 0)
            EndGame(true);
    }

    private void EndGame(bool playerDied = false)
    {
        PlayerPrefs.SetString(Keys.PlayerDied, playerDied.ToString());
        PlayerPrefs.SetInt(Keys.Score, GameScore);
        SceneManager.LoadScene(EndGameSceneName);
    }
}