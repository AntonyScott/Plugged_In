using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth = 50;
    public float damage;
    Animator animator;
    public GameObject player;
    NavMeshAgent navAI;
    Rigidbody rb;
    public float enemySpeed = 0.1f;

    public GameObject bullet;
    public bool isRanged = false;
    public Transform firePoint;
    public float myDamage = 5;
    public float bulletSpeed = 100;
    //power up variables
    public GameObject[] powerup;
    public bool runAway = true;

    public bool enemyCharge = false;
    bool haveCharged = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        navAI = GetComponent<NavMeshAgent>();
        if (isRanged)
        {
            InvokeRepeating("FireBullet", 0, 3);
        }
    }

    void Update()
    {
        //melee
        if (player != null && isRanged == false && enemyHealth > 0)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance <= 8 && haveCharged == false && enemyCharge == true)
            {
                //charge player
                Charge();
                haveCharged = true;
            }
            navAI.SetDestination(player.transform.position);
        }



        //ranged
        if (player != null && isRanged == true)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            this.transform.LookAt(targetPosition);

            if (Vector3.Distance(transform.position,player.transform.position) < 3)
            {
                //runaway
                Vector3 position = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10),transform.position.y, Random.Range(transform.position.x - 10, transform.position.x + 10));
                navAI.SetDestination(position);
                runAway = false;
            }
        }
    }
    void TakeDamage(float damageTaken, Vector3 bulPosition)
    {
        enemyHealth -= damageTaken;
        animator.SetTrigger("Hit");
        if(enemyHealth <= 0)
        {
            //powerup code
            int chance = Random.Range(0, 100);
            if (chance <= 50)
            {
                Instantiate(powerup[0], new Vector3(transform.position.x,1,transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(powerup[1], new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
            }
            //death code
            navAI.enabled = false;
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = false;
            //rb.AddForce((transform.position = bulPosition) * 2, ForceMode.Impulse);
            FindObjectOfType<AudioManager>().Play("Hit");
            Destroy(this.gameObject, 3);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {

            TakeDamage(collision.gameObject.GetComponent<BulletController>().damage, collision.transform.position);
            Destroy(collision.gameObject);
        }
    }
    void FireBullet()
    {
        GameObject myBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            myBullet.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward* bulletSpeed);
    myBullet.GetComponent<BulletController>().damage = myDamage;
        FindObjectOfType<AudioManager>().Play("enemyShoot");
    }

    void Charge()
    {
        navAI.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce((player.transform.position = transform.position) * 2, ForceMode.Impulse);
        FindObjectOfType<AudioManager>().Play("Hit");
        Invoke("FinishedCharge", 1);
    }

    void FinishedCharge()
    {
        navAI.enabled = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
