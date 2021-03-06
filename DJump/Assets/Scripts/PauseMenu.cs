﻿using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;

    public GameObject PauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !TutorialManager.TutorialPlaying)
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void LoadMenu()
    {
        GameManager.EndingGameReason = GameOverReason.Quit;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
