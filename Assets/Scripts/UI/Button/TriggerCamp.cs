using Cainos.CustomizablePixelCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerCamp : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsOfPlayer;
    [SerializeField] private GameObject[] campFire;
    [SerializeField] private Transform reSpawnPoint;
    [SerializeField] private Button buttonTrigger;
    private Scene scene;

    private GameController gameController;
    private Collider2D coll;

    private bool _isFired;
    private void Awake()
    {
        Init();    
    }
    private void Init()
    {
       //campFire = GetComponentsInChildren<GameObject>();
       // spirteRender = GetComponent<SpriteRenderer>();
       gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        coll = GetComponent<Collider2D>();
        _isFired = default;
    }
    public void CampTrigger()
    {
        if (_isFired)
        {
            return;
        }
        foreach (var item in campFire)
        {
            item.SetActive(true);
            coll.enabled = false;
            if (scene.buildIndex == 1)
            {
                foreach (var item2 in objectsOfPlayer)
                {
                    item2.SetActive(true);

                }
            }
        }

        //campFireed.SetActive(true);
        gameController.UpdateCampPoint(reSpawnPoint.position);
        _isFired = true;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           buttonTrigger.onClick.AddListener(() => CampTrigger());
            Debug.Log($"{scene.name}");
           

        }
    }
    

}
