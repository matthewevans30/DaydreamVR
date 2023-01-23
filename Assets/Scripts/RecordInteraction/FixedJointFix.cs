using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJointFix : MonoBehaviour
{
    public void onSelectEnter()
    {
        Debug.Log("Selected");
        Destroy(gameObject.GetComponent<FixedJoint>());
    }
}
