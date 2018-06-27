using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresManager : MonoBehaviour
{
    public string MainMenuSceneName;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
