using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject bossCanvas;
    public GameObject victoryCanvas;
    public GameObject player;
    public GameObject boss;
    public bool bossDeath = false;
    public 
    bool cutscenePlayed = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }
    // Update is called once per frame
    void Update()
    {
        if (cutscenePlayed == false)
        {
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            player.GetComponent<PlayerController>().enabled = false;
            boss.GetComponent<EnemyController>().enabled = false;
            Invoke("PlayCanvas", 2f);
            Invoke("CutsceneEnd", 1.5f);
            cutscenePlayed = true;

            if (bossDeath)
            {
                victoryCanvas.SetActive(true);
            }
        }
    }
    void PlayCanvas()
    {
        bossCanvas.SetActive(true);
    }

    void CutsceneEnd()
    {
        player.GetComponent<PlayerController>().enabled = true;
        boss.GetComponent<EnemyController>().enabled = true;
        bossCanvas.SetActive(false);
    }
}
