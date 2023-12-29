using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerButton : MonoBehaviour
{

    private bool _isGate;
    public int _buildIndex;
    [SerializeField] Button button;
    private void wake()
    {
        Init();
    }
    private void Init()
    {
        _isGate = default;
    }
    public  void TriggerGate()
    {
        if (_isGate)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            _isGate = false;
        }
        else { return; }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" )
        {
            _isGate = true;
            button.onClick.AddListener(
           () => TriggerGate());

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _isGate = false;
        }
    }
}
