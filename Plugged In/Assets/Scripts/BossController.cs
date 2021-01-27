using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public float enemyHealth = 50;
    public float damage;
    Animator animator;
    public GameObject player;
    Rigidbody rb;

    public GameObject bullet;

    public Transform firePoint;
    public float myDamage = 5;
    public float bulletSpeed = 100;

    bool berserkPhase = false;
    float fireTime;
    float timeBetweenFires = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        fireTime -= Time.deltaTime;
        if(fireTime <= 0)
        {
            FireBullet();
            if (berserkPhase)
            {
                fireTime = timeBetweenFires / 2;
            }
            else
            {
                fireTime = timeBetweenFires;
            }
            if(enemyHealth < 450)
            {
                berserkPhase = true;
            }
        }
    }
    void TakeDamage(float damageTaken, Vector3 bulPosition)
    {
        enemyHealth -= damageTaken;
        animator.SetTrigger("Hit");
        if (enemyHealth <= 0)
        {
            //death code
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = false;
            //rb.AddForce((transform.position = bulPosition) * 2, ForceMode.Impulse);
            FindObjectOfType<AudioManager>().Play("Hit");
            Destroy(this.gameObject, 3);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {

            TakeDamage(collision.gameObject.GetComponent<BulletController>().damage, collision.transform.position);
            Destroy(collision.gameObject);
        }
    }
    void FireBullet()
    {
        GameObject myBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        myBullet.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletSpeed);
        myBullet.GetComponent<BulletController>().damage = myDamage;
        FindObjectOfType<AudioManager>().Play("enemyShoot");
    }

}
