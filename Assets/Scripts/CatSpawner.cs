using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField] private int innerCatRadius;
    [SerializeField] private int outeraCatRadius;

    private float countdown;

    public GameObject cat;

    [HideInInspector] public int currentWaveIndex = 0;

    private int spawningWave = -1;
    private RadialCoordinateSampler CoordinateSampler;

    private void Start()
    {
        CoordinateSampler = new RadialCoordinateSampler(innerCatRadius, outeraCatRadius);
        countdown = 1f;
    }

    private void FixedUpdate()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0 && spawningWave != currentWaveIndex)
        {
            countdown = 20f;
            spawningWave = currentWaveIndex;
            int numCats = 3 + currentWaveIndex;
            StartCoroutine(SpawnWave(numCats, 2f));
        }
    }

    private IEnumerator SpawnWave(int numCats, float spawnspeed)
    {
        for (int i = 0; i < numCats; i++)
        {
            Vector2 coordinates = CoordinateSampler.SamplePoint(true);
            Vector3 spawn = new Vector3(coordinates.x, coordinates.y, 0);

            Instantiate(cat, spawn, Quaternion.identity);
            yield return new WaitForSeconds(spawnspeed);
        }

        currentWaveIndex++;
    }
}
