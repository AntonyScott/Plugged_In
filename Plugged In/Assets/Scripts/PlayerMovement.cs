using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float exposedVar = 5;
    float notExposedVar = 5;

    [SerializeField]
    float thisCanBeSeen;
    [Range(0, 100)]
    public float thisHasARange;

    void Start()
    {
        Debug.Log("This is the value " + exposedVar);
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
    }
}
