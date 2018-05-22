using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int GameScore;
    public static int GameLives;
    public static float HalfScreenHeight = 4.6f;

    public Text ScoreText;
    public int StartingLives = 1;
    public string EndGameSceneName;

    void Start()
    {
        GameLives = StartingLives;
        GameScore = 0;
    }

    void Update()
    {
        //ScoreText.text = string.Concat("Score: ", GameScore);

        if (GameLives == 0)
        {
            PlayerPrefs.SetInt("Score", GameScore);
            SceneManager.LoadScene(EndGameSceneName);
        }
    }
}