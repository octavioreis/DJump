using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Consts.MainMenuSceneName);
    }
}
