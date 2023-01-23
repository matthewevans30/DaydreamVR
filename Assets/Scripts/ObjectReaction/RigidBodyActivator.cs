using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidBodyActivator : MonoBehaviour
{
    public InputActionProperty activateRigidBody;

    private void Update()
    {
        float value = activateRigidBody.action.ReadValue<float>();

        if (value > 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
