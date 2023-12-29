using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keysNumber;
    [SerializeField] private TextMeshProUGUI booksNumber;
    public static int keys = 0;
    public static int books = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            keys++;
            keysNumber.text = "" + keys;
        } if (collision.gameObject.CompareTag("Book"))
        {
            Destroy(collision.gameObject);
            books++;
            booksNumber.text = "" + books;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
