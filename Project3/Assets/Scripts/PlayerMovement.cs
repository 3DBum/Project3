using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float groundDrag;

    public Transform orientation;
    public float playerHeight;
    
    public float JumpHeight;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump;

    public LayerMask Ground;
    bool grounded;

    public LayerMask Wall;
    public float wallForce;
    public float WallTime;
    public float WallSpeed;
    bool wallRight;
    bool wallLeft;
    bool wallRunning;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    // on start up, i may be over-commenting
    private void Start()
    {
        canJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    //goes every update
    private void Update()
    {
        inputs();
        SpeedLimit();
        WallCheck();
        WallInput();

        //makes a raycast to see if touching ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    //also every update but different
    private void FixedUpdate()
    {
        Movement();
    }

    //Player input
    private void inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Player can jump if space pressed, on ground, and not wall running
        if (Input.GetKeyDown(KeyCode.Space) && grounded && canJump && wallRunning == false)
        {
            canJump = false;

            Jump();

            //calls the jumpReset function, delayed by jumpCooldown variable
            Invoke(nameof(JumpReset), jumpCooldown);
        }
    }

    //moves the player by adding force and using the orientation input for direction where to go
    private void Movement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //changes amount of force if on air or on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    //limits players speed
    private void SpeedLimit()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpHeight, ForceMode.Impulse);

        if(wallRunning)
        {
            canJump = false;

            if (wallLeft && !Input.GetKey(KeyCode.D) || wallRight && !Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.up * JumpHeight * 1.5f);
                rb.AddForce(Vector3.up * JumpHeight * 0.5f);
            }

            rb.AddForce(orientation.forward * JumpHeight * 1f);
        }
    }
    private void JumpReset()
    {
        canJump = true;
    }

    private void WallInput()
    {
        if (Input.GetKey(KeyCode.D) && wallRight)
        {
            WallRun();
        } 
        if (Input.GetKey(KeyCode.A) && wallLeft)
        {
            WallRun();
        }
    }
    private void WallRun()
    {
        rb.useGravity = false;
        wallRunning = true;

        if (rb.velocity.magnitude <= WallSpeed)
        {
            rb.AddForce(orientation.forward * wallForce * Time.deltaTime);

            //Uses force to make the character stick to the wall
            if (wallRight)
            {
                rb.AddForce(orientation.right * wallForce / 5 * Time.deltaTime);
            }
            else
            {
                rb.AddForce(-orientation.right * wallForce / 5  * Time.deltaTime);
            }
        }
    }
    private void EndWallRun()
    {
        wallRunning = false;
        rb.useGravity = true;
    }
    private void WallCheck()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, 1f, Wall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, Wall);

        if (!wallLeft && !wallRight)
        {
            EndWallRun();
        } 
    }
}