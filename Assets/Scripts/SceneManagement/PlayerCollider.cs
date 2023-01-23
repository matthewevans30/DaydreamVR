using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
   
    public float pushForce;


    void onControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if(rb != null && !rb.isKinematic)
        {
            Vector3 force = hit.moveDirection * pushForce;
            //direction.Set(direction.x, 0, direction.z);
            rb.AddForceAtPosition(force, hit.point);
        }
    }
}
