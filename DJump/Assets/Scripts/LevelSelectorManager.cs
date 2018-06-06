using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorManager : MonoBehaviour
{
    public string Level1SceneName;
    public string Level2SceneName;
    public string Level3SceneName;

    public void StartLevel1()
    {
        SceneManager.LoadScene(Level1SceneName);
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene(Level2SceneName);
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene(Level3SceneName);
    }
}