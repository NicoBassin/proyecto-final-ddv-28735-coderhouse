using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public void NextLevel(){
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame(){
        Application.Quit();
    }
}
