using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float superSpeed;
    bool speedPowerUp = false;

    float posMaxSpeed = 10;
    float negMaxSpeed = -10;

    private Rigidbody rb;

    float movementX;
    float movementY;

    public float bulletSpeed;
    public float playerHealth = 100;
    public float curHealth;

    public float myDamage = 10;

    public GameObject bullet;
    public Transform firePoint1;
    public Transform firePoint2;

    float fireTimer = 1;
    public float fireTimerReset = 0.5f;

    public Text healthText;
    public Slider healthSlider;

    public bool isGrounded = true;

    // called first
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name == "BossScene")
        {
            transform.position = new Vector3(0, 1, 0);
        }
        //Debug.Log(mode);
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        curHealth = playerHealth;
        superSpeed = speed * 1.5f;
        //UI Update
        healthText.text = curHealth.ToString();
        healthSlider.value = curHealth;
        DontDestroyOnLoad(this.gameObject);
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
        //UI Update
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);

        if (speedPowerUp == false)
        {
            rb.AddForce(movement * speed);
        }
        else
        {
            rb.AddForce(movement * superSpeed);
        }

        //Debug.Log(rb.velocity);

        if (rb.velocity.x > posMaxSpeed)
        {
            rb.velocity = new Vector3(10, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z > posMaxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 10);
        }
        if (rb.velocity.x < negMaxSpeed)
        {
            rb.velocity = new Vector3(-10, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z < negMaxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -10);
        }
    }

    // Update is called once per frame

    void OnFire(InputValue input)
    {
        if (fireTimer <= 0)
        {
            GameObject myBullet1 = Instantiate(bullet, firePoint1.position, Quaternion.identity);
            myBullet1.GetComponent<Rigidbody>().AddForce(firePoint1.transform.forward * bulletSpeed);
            myBullet1.GetComponent<BulletController>().damage = myDamage;
            GameObject myBullet2 = Instantiate(bullet, firePoint2.position, Quaternion.identity);
            myBullet2.GetComponent<Rigidbody>().AddForce(firePoint2.transform.forward * bulletSpeed);
            myBullet2.GetComponent<BulletController>().damage = myDamage;
            fireTimer = fireTimerReset;
            FindObjectOfType<AudioManager>().Play("playerShoot");
        }
    }

    void OnJump(InputValue input)
    {
        SceneManager.LoadScene("BossScene", LoadSceneMode.Single);
        //rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        FindObjectOfType<AudioManager>().Play("Jump");
        //isGrounded = false;
        //Debug.Log("Jump!!!");
    }
    void TakeDamage(float damageTaken)
    {
        curHealth -= damageTaken;
        healthText.text = curHealth.ToString();
        healthSlider.value = curHealth;
        if (curHealth <= 0)
        {
            Destroy(this.gameObject);
            Application.Quit();
        }
    }
    private void OnCollisionEnter(Collision colllision)
    {
        if (colllision.transform.tag == "Enemy")
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedPowerup")
        {
            speedPowerUp = true;
            Invoke("NormalSpeed", 15);
            negMaxSpeed = -25;
            posMaxSpeed = 25;
            FindObjectOfType<AudioManager>().Play("speedPowerUp");
            Destroy(other.gameObject);
        }
    }

    void NormalSpeed()
    {
        speedPowerUp = false;
        negMaxSpeed = -10;
        posMaxSpeed = 10;
    }
}
