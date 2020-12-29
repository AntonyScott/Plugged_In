using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float exposedVar = 5;

    [SerializeField]
    float thisCanBeSeen;
    [Range(0, 100)]
    public float thisHasARange;

    public GameObject badGuy;

    void Start()
    {
        badGuy = GameObject.FindGameObjectWithTag("BadGuy");
        if (badGuy != null)
        {
           Debug.Log(badGuy.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (10*Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (10*Time.deltaTime));
        }
        
    }
}
