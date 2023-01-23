using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("LeftHand"))
        {
            attachTransform = leftAttachTransform;
            gameObject.tag = "LeftStick";
        }
        else if(args.interactorObject.transform.CompareTag("RightHand"))
        {
            attachTransform = rightAttachTransform;
            gameObject.tag = "RightStick";
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            gameObject.tag = "Drumstick";
        }
        else if (args.interactorObject.transform.CompareTag("RightHand"))
        {
            gameObject.tag = "Drumstick";
        }

        base.OnSelectExited(args);
    }

}
