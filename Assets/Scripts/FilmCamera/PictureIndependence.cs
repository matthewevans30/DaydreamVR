using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PictureIndependence : MonoBehaviour
{
    public void SelectEnterEventArgs()
    {
        transform.parent = null;
        Debug.Log("PickedUP");
        
    }

    public void onRelease()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("Dropped");
    }
    
}
