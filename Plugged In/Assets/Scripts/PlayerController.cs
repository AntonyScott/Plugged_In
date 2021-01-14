using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    float movementX;
    float movementY;

    public float bulletSpeed;
    public float playerHealth = 100;
    public float curHealth;

    public float myDamage = 10;

    public GameObject bullet;
    public Transform firePoint;

    float fireTimer = 1;
    public float fireTimerReset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curHealth = playerHealth;
    }
    void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    private void Update()
    {
        if (fireTimer >= 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);

        rb.AddForce(movement * speed);

        //Debug.Log(rb.velocity);

        if(rb.velocity.x > 10)
        {
            rb.velocity = new Vector3(10, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z > 10)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 10);
        }
        if (rb.velocity.x < -10)
        {
            rb.velocity = new Vector3(-10, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z < -10)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -10);
        }
    }

    // Update is called once per frame

    void OnFire(InputValue input)
    {
        if (fireTimer <= 0)
        {
            GameObject myBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            myBullet.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletSpeed);
            myBullet.GetComponent<BulletController>().damage = myDamage;
            fireTimer = fireTimerReset;
        }
    }
    
    void OnJump(InputValue input)
    {
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        Debug.Log("Jump!!!");
    }
    void TakeDamage(float damageTaken)
    {
        curHealth -= damageTaken;

        if (curHealth <= 0)
        {
            Destroy(this.gameObject);
            Application.Quit();
        }
    }
        private void OnCollisionEnter(Collision colllision)
    {
        if(colllision.transform.tag == "Enemy")
        {
            TakeDamage(10);
            print("You are taking damage!!");
        }
        if (colllision.transform.tag == "EnemyBullet")
        {
            TakeDamage(10);
            print("You are taking damage!!");
            Destroy(colllision.gameObject);
        }
    }
}
