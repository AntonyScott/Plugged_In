using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth = 50;
    public float damage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TakeDamage(float damageTaken)
    {
        enemyHealth -= damageTaken;
        if(enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletController>().damage);
            Destroy(collision.gameObject);
        }
    }
}
