using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrapple : MonoBehaviour
{

    //Detect if a grappleable object is in range

    //In what direction is this grappleable object

    //Add force to the player in the direction of the grappleable object, but going a little below 

    [SerializeField] private bool showGizmo;

    public bool canGrapple;
    public float grappleRange;
    public LayerMask grappleLayer;
    private Collider[] hitColliders;

    float distance;
    float nearestDistance;
    Vector3 closestPoint;

    Rigidbody Player;
    // Start is called before the first frame update
    void Start()
    {
        canGrapple = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If a grappleable object is in range of the player, the player can use the magnet grapple
        if (Physics.CheckSphere(transform.position, grappleRange, grappleLayer))
        {
            canGrapple = true;
        } else
        {
            canGrapple = false;
        }

        Grapple();
    }

    private void Grapple()
    {
        hitColliders = Physics.OverlapSphere(transform.position, grappleRange, grappleLayer, QueryTriggerInteraction.Collide);
        foreach(var hitCollider in hitColliders)
        {
            distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                closestPoint = hitCollider.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(distance);
            Debug.Log(closestPoint);
        }
        //if (canGrapple == true)
    }

    private void OnDrawGizmos()
    {
        if (showGizmo == true)
        {
            Gizmos.DrawWireSphere(transform.position, grappleRange);
        }
    }
}
