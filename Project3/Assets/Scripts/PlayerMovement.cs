using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody rb;

    public bool playerJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerJumping = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerJumping == false) //if player is not on ground layer
        {
            rb.velocity = new Vector3(0, 5, 0);
            playerJumping = true;
        }
        
    }
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerJumping = false;
    }
}