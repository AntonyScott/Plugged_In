using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth = 50;
    public float damage;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void TakeDamage(float damageTaken)
    {
        enemyHealth -= damageTaken;
        animator.SetTrigger("Hit");
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
