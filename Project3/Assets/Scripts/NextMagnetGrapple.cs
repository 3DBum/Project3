using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NextMagnetGrapple : MonoBehaviour
{

    //THIS IS THE NEXT ITERATION OF MAGNET GRAPPLE



    //Detect if a grappleable object is in range

    //In what direction is this grappleable object

    //Add force to the player in the direction of the grappleable object, but going a little below 

    public PlayerInputActions playerControls;
    [SerializeField] private bool showGizmo;
    public bool canGrapple;
    public bool grounded;
    public float grappleRange;
    public float gravity;
    public float cooldown;
    [Header("Remaining Upward Velocity when touching grapple point")]
    public float fVelocity;
    public LayerMask grappleLayer;
    public PlayerMovement movement;
    Rigidbody rb;

    bool grappleDetected;
    float distance;
    float nearestDistance;
    Vector3 closestPoint;
    Vector3 direction;

    [SerializeField] private AudioClip grappleSound;
    private Collider[] hitColliders;
    private InputAction grapple;

    #region Player Controls Spaghetti

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        grapple = playerControls.Player.Grapple;
        grapple.Enable();
        grapple.performed += InputGrapple;
    }

    private void OnDisable()
    {
        grapple.Disable();
    }

    #endregion

    void Start()
    {
        canGrapple = true;
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.up * gravity;
    }

    void Update()
    {
        grounded = GetComponent<PlayerMovement>().grounded;

        //If a grappleable object is in range of the player, the player can use the magnet grapple
        if (Physics.CheckSphere(transform.position, grappleRange, grappleLayer))
        {
            grappleDetected = true;
            //Debug.Log("Can Grapple");
        }
        else
        {
            grappleDetected = false;
        }
    }

    private void InputGrapple(InputAction.CallbackContext context)
    {
        if (canGrapple && grappleDetected)
        {
            Grapple();
        }
    }

    private void Grapple()
    {
        //Go through grapple objects within range of the player and pick the closest one
        nearestDistance = Mathf.Infinity;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grappleRange, grappleLayer, QueryTriggerInteraction.Collide);
        foreach (Collider c in hitColliders)
        {
            var point = c.transform.position;
            distance = Vector3.Distance(transform.position, c.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                closestPoint = point;
            }
        }
        canGrapple = false;
        rb.velocity = CalculateVelocity();
        SoundManager.instance.PlaySoundClip(grappleSound, transform, 0.8f);
        print(CalculateVelocity());
        StartCoroutine(GrappleCoolDown());
    }

    //Using Kinematic Equations, calculate the starting velocity needed in the Vertical and Horizontal directions to maintain fVelocity when passing through the Grapple Point
    Vector3 CalculateVelocity()
    {
        float displacementY = closestPoint.y - transform.position.y;
        Vector3 displacementXZ = new Vector3(closestPoint.x - transform.position.x, 0, closestPoint.z - transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt((fVelocity * fVelocity) - (2 * gravity * displacementY));
        float time = ((fVelocity - velocityY.y) / gravity);
        Vector3 velocityXZ = (displacementXZ / time);

        return velocityXZ + velocityY;
    }

    IEnumerator GrappleCoolDown()
    {
        yield return new WaitForSeconds(cooldown);
        canGrapple = true;
    }

    //This just shows the grapple range gizmos
    private void OnDrawGizmos()
    {
        if (showGizmo == true)
        {
            Gizmos.DrawWireSphere(transform.position, grappleRange);
        }
    }
}
