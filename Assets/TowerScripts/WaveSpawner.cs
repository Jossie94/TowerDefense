using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    //public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;

    private float countdown = 10f;

    private int waveNumber = 0;

    public Text wavecountdowntext;

    public Wave[] waves;

    void Update()
    {
        if(EnemiesAlive >0)
        {
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine (SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        wavecountdowntext.text = string.Format("{0:00}", countdown);

        //wavecountdowntext.text = Mathf.Round(countdown).ToString(); //cuts off decimals when converting to string from float

    }

    IEnumerator SpawnWave()
    {
       
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;

        //Debug.Log("Wave Incoming!");
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

}
