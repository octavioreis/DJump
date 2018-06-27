using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static MainMenuManager.Levels CurrentLevel;
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

        CurrentLevel = (MainMenuManager.Levels)Enum.Parse(typeof(MainMenuManager.Levels), PlayerPrefs.GetString(MainMenuManager.CurrentLevelKey));
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
                case MainMenuManager.Levels.Level1:
                    if (GameScore >= DistanceNeededToFinishLevel1)
                        EndGame();
                    break;
                case MainMenuManager.Levels.Level2:
                    if (GameScore >= DistanceNeededToFinishLevel2)
                        EndGame();
                    break;
                case MainMenuManager.Levels.Level3:
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