using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : MonoBehaviour
{
    public GameObject player;
    public float myDamage;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //P = player.GetComponent<PlayerController>();
    }


    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamagePickup();
        }
    }
    void DamagePickup()
    {
        Debug.Log("Powerup picked up");
        GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("Damage", 15);
        FindObjectOfType<AudioManager>().Play("damagePowerUp");
        Destroy(gameObject);
    }
    void Damage()
    {
        myDamage = PlayerController.myDamage;
        myDamage = 50;
    }
}
