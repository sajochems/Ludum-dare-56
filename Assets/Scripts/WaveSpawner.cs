using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private int innerEnemyRadius;
    [SerializeField] private int outerEnemyRadius;

    private float countdown;
    private LinkedList<Wave> waves;

    public GameObject enemy;

    [HideInInspector] public int currentWaveIndex = 0;

    private int spawningWave = -1;
    private RadialCoordinateSampler CoordinateSampler;

    private bool doneSpawning;

    private void Start()
    {
        CoordinateSampler = new RadialCoordinateSampler(innerEnemyRadius, outerEnemyRadius);

        waves = new LinkedList<Wave>();
        waves.AddFirst(new Wave(6, 2, 30));

        countdown = 5;
        doneSpawning = true;
    }
    
    private void FixedUpdate()
    {
        
        if(currentWaveIndex >= 16)
        {
            GameState.EndGame();
        }
        if(countdown > 0)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 10 && GameState.enemiesLeft > 0)
        {
            //Always have a build phase 
            countdown = 10;
            return;
        }

        if (countdown <= 0 && spawningWave != currentWaveIndex)
        {        
            countdown = waves.First.Value.timeToNextWave;
            StartCoroutine(SpawnWave());
            GenerateNextWave();
        }

        if(GameState.enemiesLeft <= 0)
        {
            if( spawningWave != currentWaveIndex) {
                GameState.SwitchState("build");
            }
        } else
        {
            GameState.SwitchState("fight");
        }
    }

    private IEnumerator SpawnWave()
    {
        Wave wave = waves.First.Value;
        waves.RemoveFirst();
        spawningWave = currentWaveIndex;

        for (int i = 0; i < wave.numEnemies; i++)
        {
            GameState.enemiesLeft += 1;

            Vector2 coordinates = CoordinateSampler.SamplePoint(true);
            Vector3 spawn = new Vector3(coordinates.x, coordinates.y, 0);
    
            Instantiate(enemy, spawn, Quaternion.identity);
            yield return new WaitForSeconds(wave.timeToNextEnemy);
        }

        currentWaveIndex++;     
    }

    private void GenerateNextWave() {
        int numEnemies = 6 + (int)Math.Pow(2, currentWaveIndex + 1);
        float timeToNextEnemy = Mathf.Max(0.001f, 2f - ( 0.2f * (float)currentWaveIndex+1));
        int timeToNextWave = Mathf.Max(25, (30 - currentWaveIndex));
        waves.AddLast(new Wave(numEnemies, timeToNextEnemy, timeToNextWave));
    }
}

[System.Serializable]
public class Wave
{
    public int numEnemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    public Wave(int ne, float tn, float tw)
    {
        numEnemies = ne;
        timeToNextEnemy = tn;
        timeToNextWave = tw;
    }
}
