using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterBed : MonoBehaviour
{
    public TransitionManager transitionManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Trigger Entered");
            transitionManager.GoToScene(2);

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
