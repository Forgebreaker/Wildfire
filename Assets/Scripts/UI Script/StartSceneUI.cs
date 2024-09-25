using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject tutorialUI;
    public void OnClickPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClickTutorial()
    {
        mainUI.SetActive(false);
        tutorialUI.SetActive(true);
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    public void OnClickBack()
    {
        tutorialUI.SetActive(false);
        mainUI.SetActive(true);
    }    
}
