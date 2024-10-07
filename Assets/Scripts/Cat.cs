using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public GameObject catPrefab;

    private AudioSource audioSource;
    private int randomSeed;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        randomSeed = rand.Next(0, 1000);
        audioSource = GetComponent<AudioSource>();
        float pitch = 1f + ((float)rand.Next(-20, 20) * 0.01f);
        audioSource.pitch = pitch;
    }

    // Update is called once per frame
    public void Update()
    {
        float y = Mathf.Sin(Time.time + randomSeed) / 5000;
        Vector3 pos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(pos.x, (pos.y + y), pos.z);
    }

    public void GrabThatCat()
    {
        GameState.IncreaseCats(1);
        GameState.IncreaseScore(10);
        Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
        Instantiate(catPrefab, position, transform.rotation);

        GetComponent<AudioSource>().Play();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Invoke("DeleteObject", 5);
    }

    private void DeleteObject() {
        Destroy(gameObject);
    }
}
