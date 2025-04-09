using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public Transform orientation;

    public float rotationSpeed;

    private PlayerInputActions playerControls;
    private InputAction move;

    float tempHorizontal;
    float tempVertical;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    #region Player Controls Spaghetti

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        var tempMove = move.ReadValue<Vector2>();
        tempHorizontal = tempMove.x;
        tempVertical = tempMove.y;

        Vector3 inputDir = orientation.forward * tempVertical + orientation.right * tempHorizontal;

        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }
}
