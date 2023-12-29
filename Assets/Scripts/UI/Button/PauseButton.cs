using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PauseButton : MonoBehaviour
{
    //[SerializeField] private static bool GameIsPause = false;
    [SerializeField] private GameObject PauseGameMenu;
    [SerializeField] private GameObject SaveGameMenu;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite resumeSprite;
    //public void PauseGameTrigger()
    //{
    //    if (GameIsPause)
    //    {
    //        ResumeGame();
    //    }
    //    else
    //    {
    //        PauseGame();
    //    }
    //}
    //public 
    public void ResumeGame()
    {
        PauseGameMenu.SetActive(false);
        Time.timeScale = 1.0f;
        //GameIsPause = false;
        pauseButton.image.sprite = resumeSprite;
    }
    public void PauseGame()
    {
        PauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        //GameIsPause = true;
        pauseButton.image.sprite = pauseSprite;

    } 
    
    public void BackGame()
    {
        SaveGameMenu.SetActive(false);
        Time.timeScale = 1.0f;
        //GameIsPause = false;
    }


}
