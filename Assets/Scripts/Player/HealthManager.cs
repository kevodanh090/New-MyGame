using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health;

    [SerializeField] private float fillTime = 10;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private const int maxOfHeart = 5;
    private float coolDownTime;
    

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        health = maxOfHeart;
        coolDownTime = fillTime;
    }
    private void Update()
    {
        checkHeart(ref health, maxOfHeart, ref coolDownTime,  fillTime);
     
        for (int i = 0; i < hearts.Length; i++)
        {
            if( i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if( i < maxOfHeart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    private void checkHeart(ref int health, int maxOfHeart, ref float coolDownTime, float fillTime)
    {
        if (health >= maxOfHeart)
        {
            health = maxOfHeart;
        }
        else
        {
            FillHeart(ref health, ref coolDownTime, fillTime);
        }

    }
    private int FillHeart(ref int health, ref float coolDownTime, float fillTime)
    {
        if (coolDownTime > 0)
        {
            coolDownTime -= Time.deltaTime;
        }
        if (coolDownTime < 0)
        {
            coolDownTime = 0;
        }
        if (coolDownTime == 0)
        {
            health += 1;
            coolDownTime = fillTime;
        }
        return health;
    }
  
}
