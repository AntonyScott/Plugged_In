using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float myDamage = 5;
    public float otherDamage = 0;
    public GameObject Sphere;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        otherDamage = Sphere.GetComponent<GetDamage>().damage;
    }
}
