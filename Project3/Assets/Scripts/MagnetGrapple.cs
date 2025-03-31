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
    private Rigidbody rb;

    float distance;
    float nearestDistance;
    Vector3 closestPoint;
    Vector3 point;

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
            Debug.Log("Can Grapple");
        } else
        {
            canGrapple = false;
        }

        Grapple();
    }

    private void Grapple()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grappleRange, grappleLayer, QueryTriggerInteraction.Collide);
        foreach (Collider c in hitColliders)
        {
            point = c.transform.position;
            distance = Vector3.Distance(transform.position, c.transform.position);

            if (nearestDistance != distance)
            {
                
            }

            //Find a way to find the closest point and store that value, using an if (distance < closestDistance) does not account for
            //if the player moves away and close to another collider.


        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo == true)
        {
            Gizmos.DrawWireSphere(transform.position, grappleRange);
        }
    }
}
