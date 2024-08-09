using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject AudioMenu;

    public void OnPause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        AudioMenu.SetActive(false);
    }

    public void OnClose(int closeWindow)
    {
        if(closeWindow == 0)
            OnResume();
        if(closeWindow == 1)
            AudioMenu.SetActive(false);
    }

    public void OnResume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        AudioMenu.SetActive(false);
    }

    public void OnAudio()
    {
        AudioMenu.SetActive(true);
    }
}
