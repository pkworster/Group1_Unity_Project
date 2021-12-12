using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting };
    [System.Serializable]
    // what the wave should be... defined
    public class Wave
    {
        // name ref for later UI stuff and things
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }
    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private float searchCountdown = 1f;
    public SpawnState state = SpawnState.counting;
    public Transform[] spawnPoints;



    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.waiting)
        {
            // checking enemies
            if (!EnemyIsAlive())
            {
                //new wave
                
                WaveCompleted();

            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                // start wave spawn
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        Debug.Log("virus neutralized!");
        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All viruse eliminated! Get ready to start again...");
        }
        else
        {
            nextWave++;
        }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning spawnage:" + _wave.name);
        state = SpawnState.spawning;
        // more spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/ _wave.rate);

        }
        state = SpawnState.waiting;
        yield break;
    }
    void SpawnEnemy (Transform _enemy)
    {
        //spawn the enemy
        Debug.Log("Spawnage:" + _enemy.name);
        //check
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point my dude!!!");
        }
        //spawn points
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        // enemy
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
