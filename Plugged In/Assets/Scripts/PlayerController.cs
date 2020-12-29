using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    float movementX;
    float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);

        rb.AddForce(movement * speed);

        Debug.Log(rb.velocity);

        if(rb.velocity.x > 10)
        {
            rb.velocity = new Vector3(10, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z > 10)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 10);
        }
    }

    // Update is called once per frame

    void OnFire(InputValue input)
    {
        Debug.Log("FIRE!!!");
    }
    
    void OnJump(InputValue input)
    {
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        Debug.Log("Jump!!!");
    }
}
