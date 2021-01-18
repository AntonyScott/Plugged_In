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
    public float bulletSpeed = 1000;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        navAI = GetComponent<NavMeshAgent>();
        if (isRanged)
        {
            InvokeRepeating("FireBullet", 0, 1);
        }
    }

    void Update()
    {
        if (player != null && isRanged == false)
        {
            navAI.SetDestination(player.transform.position);
            // transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, enemySpeed);
        }
        if (player != null && isRanged == true)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            this.transform.LookAt(targetPosition);

            if(Vector3.Distance(transform.position,player.transform.position) < 3)
            {
                Vector3 position = new Vector3(Random.Range(transform.position.x-2, transform.position.x + 2),transform.position.y, Random.Range(transform.position.x - 2, transform.position.x - 2));
                navAI.SetDestination(position);
            }
        }
    }
    void TakeDamage(float damageTaken, Vector3 bulPosition)
    {
        enemyHealth -= damageTaken;
        animator.SetTrigger("Hit");
        if(enemyHealth <= 0)
        {
            navAI.enabled = false;
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = false;
            rb.AddForce((transform.position = bulPosition) * 2, ForceMode.Impulse);
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
        FindObjectOfType<AudioManager>().Play("Shoot");
    }

}
