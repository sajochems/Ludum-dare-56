using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCatShop : MonoBehaviour
{
    public GameObject fireCatPrefab;

    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }
    public void BuyCat()
    {
        GameState.DecreaseCatfood(2000);
        GameState.DecreaseCats(1);
        Instantiate(fireCatPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvas.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvas.SetActive(false);
        }
    }
}
