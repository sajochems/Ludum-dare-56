using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countdown;

    [SerializeField] private GameObject spawnPoint;

    public Wave[] waves;

    [HideInInspector] public int currentWaveIndex = 0;

    private int spawningWave = -1;

    private void Update()
    {
        if(currentWaveIndex >= waves.Length)
        {
            GameState.SwitchState("build");
            return;
        }

        if(countdown > 0)
        {
            countdown -= Time.deltaTime;
        }        

        if(countdown <= 0 && spawningWave != currentWaveIndex)
        {        
            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }

        if(GameState.enemiesLeft <= 0)
        {
            GameState.SwitchState("build");
        } else
        {
            GameState.SwitchState("fight");
        }
    }

    private IEnumerator SpawnWave()
    {
        if(currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                spawningWave = currentWaveIndex;
                GameState.enemiesLeft += 1;
                GameObject enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);
                enemy.transform.SetParent(spawnPoint.transform);          
                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }

            currentWaveIndex++;
        }      
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;
}
