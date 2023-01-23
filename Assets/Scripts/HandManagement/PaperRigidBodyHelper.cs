using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperRigidBodyHelper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("RightHand") || other.CompareTag("LeftHand")))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log("RigidBodyActivated");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand") || other.CompareTag("LeftHand"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log("RigidBodyDeactivated");
        }

    }


}
