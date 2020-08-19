using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [System.Serializable]  // this struct will show inside unity inspector 
    public class Wave
    {
        public Enemy[] enemies; //tyypes
        public int count; //how many (relvent for random only)
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spwanPoints; // where enemies can spwan
    public float timeBetweenWaves;

    private Wave currentWave; 
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawning;

    public GameObject boss;
    public Transform bossSpawnPoint;

    public GameObject healthBar;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];
        //for random remove this:
        currentWave.count = currentWave.enemies.Length;
        for (int i = 0; i < currentWave.count; i++)
        {
            if(player == null)
            {
                yield break;
            }

            //for random use this:
            //Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)]; // choose random enemy;
            Enemy randomEnemy = currentWave.enemies[i];
            Transform randomSpot = spwanPoints[Random.Range(0, spwanPoints.Length)];//place to spawn
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation); //spawn

            if (i == currentWave.count - 1)
            {
                finishedSpawning = true;
            }
            else 
            {
                finishedSpawning = false;
            }
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) //no enemies and no more spawning, the lvl is done
        { // move to next lvl
            finishedSpawning = false;
            if(currentWaveIndex + 1 < waves.Length) // there more lvls
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else 
            {
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                healthBar.SetActive(true);
            }
        }
    }
}
