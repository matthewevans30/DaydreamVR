using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulbReplacement : MonoBehaviour
{
    public XRBaseController RightController, LeftController;

    [ColorUsage(true, true)]
    public Color originalEmissionColor;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        gameObject.tag = "Untagged";
        RightController.SendHapticImpulse(1f, 0.5f);
        LeftController.SendHapticImpulse(1f, 0.5f);

        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", (1 * originalEmissionColor));
        gameObject.transform.GetChild(0).GetComponent<Light>().enabled = false; 
        args.interactableObject.transform.GetComponent<Light>().enabled = true;        
    }
}

