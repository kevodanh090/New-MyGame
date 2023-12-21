using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // chuyển tới scene +1 (scene main menu ở 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }    
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
