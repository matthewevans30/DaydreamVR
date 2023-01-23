using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody _rb;
    public bool leftHand;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //position
        _rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //rotation
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        _rb.angularVelocity = ((rotationDifferenceInDegree * Mathf.Deg2Rad) / Time.fixedDeltaTime);
        
    }
}
