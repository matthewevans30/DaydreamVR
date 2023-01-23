using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(XROrigin))]

public class RoomScaleFix : MonoBehaviour
{

    private CharacterController character;
    private XROrigin xrOrigin;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        character.height = xrOrigin.CameraInOriginSpaceHeight + 0.15f;      //change controller height to camera z location + top of head

        var centerPoint = transform.InverseTransformPoint(xrOrigin.Camera.transform.position);
        character.center = new Vector3(centerPoint.x, character.height / 2 + character.skinWidth, centerPoint.z);

        character.Move(new Vector3(0.001f, -0.001f, 0.001f));
        character.Move(new Vector3(-0.001f, 0.001f, -0.001f));      //update physics by moving controller slightly
    }
}
