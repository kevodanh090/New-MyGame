using Cainos;
using Cainos.CustomizablePixelCharacter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    private PixelCharacter fx;
    [ExposeProperty]                                        // is the character dead? if dead, plays dead animation and disable control
    public bool IsDead
    {
        get { return isDead; }
        set
        {
            isDead = value;
            fx.IsDead = isDead;
            //fx.DropWeapon();
        }
    }
    public static bool isDead;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        fx = GetComponent<PixelCharacter>();
    }

    private void Start()
    {
        checkPointPos = transform.position;
    }
    public void UpdateCampPoint(Vector2 pos)
    {
        checkPointPos = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            
            Die();

        }
    }

    private void Die()
    {
        IsDead = true;
        //fx.DropWeapon();
        Debug.Log($"HealthManager.health = {HealthManager.health}");
        HealthManager.health--;
        StartCoroutine(Respawn(2.0f));
    }
    IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        IsDead = false;
        //fx.AddWeapon(weapon,true);
        
        transform.position = checkPointPos;
    }

}
