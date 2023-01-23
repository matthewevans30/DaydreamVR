using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDelay : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
        //StartCoroutine(DelayReactivity());
    }

    IEnumerator DelayReactivity()
    {
        yield return new WaitForSeconds(5);
        audioSource.Play();
        AudioParameterDriver.active = true;
        AudioParameterDriver.recoverySpeed = 0.915f;

    }
}
