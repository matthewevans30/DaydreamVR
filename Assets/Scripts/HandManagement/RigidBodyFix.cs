using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyFix : MonoBehaviour
{
    public void OnSelectEnter()
    {
        PausePlayButton.armIsHeld = true;
    }
    public void OnSelectExit()
    {
        PausePlayButton.armIsHeld = false;
    }

}
