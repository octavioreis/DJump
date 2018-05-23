using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //public Text Score;
    public string MainMenuSceneName;

    private int _playerScore;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private void Start()
    {
        //playerScore = PlayerPrefs.GetInt("Score");
    }

    private void Update()
    {
        //Score.text = string.Concat("Final Score: ", playerScore);
    }
}
