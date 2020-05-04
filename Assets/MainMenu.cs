using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitProg()
    {
        Application.Quit();
    }

    public void LoadRoom()
    {
       SceneManger.LoadScene(1);
    }
}
