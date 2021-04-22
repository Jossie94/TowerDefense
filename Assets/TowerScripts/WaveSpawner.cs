using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;

    private float countdown = 2f;

    private int waveNumber = 0;

    public Text wavecountdowntext;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine (SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        wavecountdowntext.text = Mathf.Round(countdown).ToString(); //cuts off decimals when converting to string from float

    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        
        //Debug.Log("Wave Incoming!");
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
