using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static bool TutorialPlaying;

    public GameObject TutorialPanel1;
    public GameObject TutorialPanel2;
    public GameObject TutorialPanel3;

    private void Update()
    {
        if (!SaveManager.Instance.TutorialCompleted && !TutorialPlaying)
            StartTutorial();
    }

    public void StartTutorial()
    {
        TutorialPlaying = true;
        Time.timeScale = 0f;
        LoadTutorialPage1();
    }

    public void LoadTutorialPage1()
    {
        TutorialPanel2.SetActive(false);
        TutorialPanel1.SetActive(true);
    }

    public void LoadTutorialPage2()
    {
        TutorialPanel1.SetActive(false);
        TutorialPanel3.SetActive(false);
        TutorialPanel2.SetActive(true);
    }

    public void LoadTutorialPage3()
    {
        TutorialPanel2.SetActive(false);
        TutorialPanel3.SetActive(true);
    }

    public void EndTutorial()
    {
        TutorialPanel3.SetActive(false);
        TutorialPlaying = false;
        Time.timeScale = 1f;

        SaveManager.Instance.TutorialCompleted = true;
        SaveManager.Instance.Save();
    }
}
