using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticController : MonoBehaviour
{
    public XRBaseController leftController, rightController;

    public void SendHaptics(bool leftHand, bool rightHand, float amplitude, float duration)
    {
        if (leftHand)
        {
            leftController.SendHapticImpulse(amplitude, duration);
        }

        if (rightHand)
        {
            rightController.SendHapticImpulse(amplitude, duration);
        }
    }
}
