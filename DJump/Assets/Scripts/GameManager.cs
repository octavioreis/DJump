using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Levels CurrentLevel;
    public static bool IsFreeRun;
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

    public void Start()
    {
        bool isFreeRun;

        if (!bool.TryParse(PlayerPrefs.GetString(MainMenuManager.FreeRunKey), out isFreeRun))
            isFreeRun = true;

        CurrentLevel = (Levels)Enum.Parse(typeof(Levels), PlayerPrefs.GetString(MainMenuManager.CurrentLevelKey));
        IsFreeRun = isFreeRun;
        GameLives = StartingLives;
        GameScore = 0;
    }

    public void Update()
    {
        ScoreText.text = string.Concat("Score: ", GameScore);

        if (!IsFreeRun)
        {
            switch (CurrentLevel)
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
            EndGame();
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt(MainMenuManager.ScoreKey, GameScore);
        SceneManager.LoadScene(EndGameSceneName);
    }
}