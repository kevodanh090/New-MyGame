using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int hearts = HealthManager.health;
    public int keys = ItemCollector.keys;
    public int books = ItemCollector.books;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        hearts = data.hearts;
        keys = data.keys;
        books = data.books;

        Vector3 pos;
        pos.x = data.position[0];
        pos.y = data.position[1];
        pos.z = data.position[2];
        transform.position = pos;

        SceneManager.LoadScene("Village");

    }
    public void LoadData()
    {

    }
}
