using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeUIVisible : MonoBehaviour
{
    public GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("RightHand") || other.gameObject.CompareTag("LeftHand"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand") || other.gameObject.CompareTag("LeftHand"))
        {
            canvas.SetActive(false);
        }
    }
}
