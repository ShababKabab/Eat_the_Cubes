using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        // ScoreManager.scoreCount = 0 ;
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame(){
        Application.OpenURL("https://shabab-kabab.itch.io/eat-the-cube");
    }
}
