using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int hearts;
    public int keys;
    public int books;
    public float[] position;

    public PlayerData (Player player)
    {
        hearts = player.hearts;
        keys = player.keys;
        books = player.books;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
