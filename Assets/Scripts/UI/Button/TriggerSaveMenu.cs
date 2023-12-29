using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSaveMenu : MonoBehaviour
{
    [SerializeField] private GameObject saveGameMenu;
    [SerializeField] private Button btnTrigger;
    
    private void SaveMenu()
    {
            saveGameMenu.SetActive(true);
            Time.timeScale = 0f;
            //GameIsPause = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            btnTrigger.onClick.AddListener(() => SaveMenu());
        }
    }
}
