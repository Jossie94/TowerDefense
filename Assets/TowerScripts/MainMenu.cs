using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string leveltoLoad = "MainScene";

    public void Play()
    {
        SceneManager.LoadScene(leveltoLoad);
        Debug.Log("Play");
    }

    public void Quit ()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }
}
