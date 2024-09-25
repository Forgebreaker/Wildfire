using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUIManager : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    public PlayerHealth Player1;   
    public PlayerHealth Player2;
    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
    public void BackToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");

    }
    public void ShowWinner()
    {
        if (Player1.CurrentHealth == 0)  
        {
            SceneManager.LoadScene("WinScenePlayerRed");
        }
        else if (Player2.CurrentHealth == 0)  
        {
            SceneManager.LoadScene("WinScenePlayerBlue");
        }
    }
}
