using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{

    public void goBack(Button b)
    {
        if(b.name.Equals("Back"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
