using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputActions playerControls;
    public float moveSpeed;
    public float speedLimit;

    public float groundDrag;

    public Transform orientation;
    public float playerHeight;
    
    public float JumpHeight;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump;

    public LayerMask Ground;
    public bool grounded;

    public LayerMask Wall;
    public float wallForce;
    public float WallTime;
    public float WallSpeed;
    public bool wallRight;
    public bool wallLeft;
    public bool wallRunning;

    float horizontalInput;
    float verticalInput;
    float tempHorizontal;
    float tempVertical;

    Vector3 moveDirection;
    Rigidbody rb;

    private InputAction move;
    private InputAction WallRunLeft;
    private InputAction WallRunRight;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    #region Player Controls Spaghetti

    private void OnEnable()
    {
        WallRunLeft = playerControls.Player.WallRunLeft;
        WallRunRight = playerControls.Player.WallRunRight;
        move = playerControls.Player.Move;

        WallRunLeft.Enable();
        WallRunRight.Enable();
        move.Enable();

        WallRunLeft.performed += WallInput;
        WallRunRight.performed += WallInput;
    }

    private void OnDisable()
    {
        WallRunLeft.Disable();
        WallRunRight.Disable();
        move.Disable();
    }

    #endregion

    private void Start()
    {
        moveSpeed = 7;
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

        //makes a raycast to see if touching ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //Player input
    private void inputs()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal"); //X values
        //verticalInput = Input.GetAxisRaw("Vertical"); //Y values

        var tempMove = move.ReadValue<Vector2>();
        tempHorizontal = tempMove.x;
        tempVertical = tempMove.y;

        //Player can jump if space pressed, on ground, and not wall running
        if (Input.GetKeyDown(KeyCode.Space) && grounded && canJump && wallRunning == false)
        {
            canJump = false;

            Jump();

            //Resets jump
            Invoke(nameof(JumpReset), jumpCooldown);
        }
    }

    //moves the player by adding force and using the orientation input for direction on where to go
    private void Movement()
    {
        moveDirection = orientation.forward * tempVertical + orientation.right * tempHorizontal;

        //changes the amount of force if in the air or on the ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    //limits the player's speed
    private void SpeedLimit()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speedLimit)
        {
            Vector3 limitedVel = flatVel.normalized * speedLimit;
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

            rb.AddForce(orientation.forward * JumpHeight * 1f);
        }
    }
    private void JumpReset()
    {
        canJump = true;
    }

    private void WallInput(InputAction.CallbackContext context)
    {
        if (wallRight)
        {
            WallRun();
        } 
        if (wallLeft)
        {
            WallRun();
        }
    }

    private void WallRun()
    {
        rb.useGravity = false;
        wallRunning = true;
        moveSpeed = 13;

        if (rb.velocity.magnitude <= WallSpeed)
        {
            rb.AddForce(orientation.forward * wallForce * Time.deltaTime);

            //Uses force to make the player stick to the wall
            if (wallRight)
            {
                rb.AddForce(orientation.right * wallForce / 5 * Time.deltaTime);

                if(Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
                {
                    moveSpeed = 0;
                }
            }
            else
            {
                rb.AddForce(-orientation.right * wallForce / 5  * Time.deltaTime);

                if(Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
                {
                    moveSpeed = 0;
                }
            }
        }
    }
    private void EndWallRun()
    {
        wallRunning = false;
        rb.useGravity = true;
        moveSpeed = 7;
    }
     private void WallCheck()
     {
         wallRight = Physics.Raycast(transform.position, orientation.right, 1f, Wall);
         wallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, Wall);

         if (!wallLeft && !wallRight)
         {
             EndWallRun();
         } 

         if (wallLeft && rb.velocity.magnitude == 0 || wallRight && rb.velocity.magnitude == 0)
         {
             EndWallRun();
         }
     }


}