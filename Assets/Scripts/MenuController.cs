using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public void changeScene(Button b)
    {
        if (b.name.Equals("Start"))
        {
            SceneManager.LoadScene("Game");
        }
        if (b.name.Equals("Help"))
        {
            SceneManager.LoadScene("Help");
        }
        if (b.name.Equals("Exit"))
        {
            Application.Quit();
        }
    }
}
