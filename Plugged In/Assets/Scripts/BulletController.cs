using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    GameObject player;
    public bool bossBullet;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(this.gameObject, 4);
    }
    void update()
    {
        if (!bossBullet)
        {
            Vector3 target = Vector3.Lerp(transform.position, player.transform.position, 0.05f);
            transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
        }
        else
        {
            Vector3 target = Vector3.Lerp(transform.position, player.transform.position, 0.5f);
            GetComponent<Rigidbody>().AddForce((target - transform.position) * 10);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
