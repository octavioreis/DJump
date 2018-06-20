using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text Score;
    public string MainMenuSceneName;

    private int _playerScore = 0;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private void Start()
    {
        _playerScore = PlayerPrefs.GetInt("Score");
    }

    private void Update()
    {
        Score.text = string.Concat("Final Score: ", _playerScore);
    }
}
