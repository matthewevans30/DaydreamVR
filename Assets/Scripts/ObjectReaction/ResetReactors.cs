using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetReactors : MonoBehaviour
{
    public List<GameObject> reactors = new List<GameObject> ();

    private void Start()
    {
        foreach(GameObject reactor in reactors)
        {
            reactor.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }
}
