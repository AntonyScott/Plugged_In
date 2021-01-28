using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    int waveNumber = 0;
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public float waveTimer = 30;
    float waveCurrent;
    bool waveOver = false;
    int counter = 0;
    float numberOfEnemies = 3;

    //enemies spawn when game level starts
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        waveCurrent -= Time.deltaTime;
        if (waveCurrent <= 0)
        {
            SpawnWave();
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            if (waveNumber <= 4)
            {
                SpawnWave();
            }
            else
            {
                print("You win, NICE!");
                SceneManager.LoadScene("BossScene", LoadSceneMode.Single);
            }
        }
        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //this instantiates enemies to their spawnpoints, every wave spawns an extra 2 enemies
    void SpawnWave()
    {
        if(waveNumber <= 4)
        while (counter < numberOfEnemies)
        {
            Instantiate(enemies[0], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            counter++;
        }
        numberOfEnemies += 5;
        waveCurrent = 30;
        waveNumber++;
    }
}
