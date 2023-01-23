using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidBodyActivator : MonoBehaviour
{
    public GameObject rightHand, leftHand;

    bool rightHold;
    bool leftHold;

    // turn off rigid bodies on hands when object is held in order to avoid
    // collision while holding
    public void OnSelected()
    {
        leftHand.transform.GetChild(0).gameObject.SetActive(true);
        leftHand.transform.GetChild(1).gameObject.SetActive(false);
        
        rightHand.transform.GetChild(0).gameObject.SetActive(true);
        rightHand.transform.GetChild(1).gameObject.SetActive(false);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.CompareTag("RightHand")))
        {
            rightHold = true;
        }

        if ((collision.collider.CompareTag("LeftHand")))
        {
            leftHold = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.collider.CompareTag("LeftHand")))
        {
            leftHold = false;
        }

        if ((collision.collider.CompareTag("RightHand")))
        {
            rightHold = false;
        }
    }
    
    // re enable hand rigid body after object is released
    // update to async instead of coroutine
    public void OnExited()
    {
        StartCoroutine("RigidBodyResumeDelay");
    }

    public IEnumerator RigidBodyResumeDelay()
    {
        yield return new WaitForSeconds(0.5f);

        if (leftHold)
        {
            leftHand.transform.GetChild(0).gameObject.SetActive(false);
            leftHand.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (rightHold)
        {
            rightHand.transform.GetChild(0).gameObject.SetActive(false);
            rightHand.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
