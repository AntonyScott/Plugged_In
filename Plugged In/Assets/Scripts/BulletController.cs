using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;

    void Start()
    {
        Destroy(this.gameObject, 4);
    }
    void update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
