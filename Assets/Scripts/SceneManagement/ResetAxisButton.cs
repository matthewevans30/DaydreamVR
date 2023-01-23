using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetAxisButton : ButtonVR
{
    AudioSource sound;
    public AudioClip down;
    public AudioClip up;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        sound = GetComponent<AudioSource>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void RightObjects()
    {
        
        foreach(GameObject reactor in AudioParameterDriver.reactorList)
        {
            reactor.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            reactor.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        }
        Debug.Log("Objects Righted");
    }
    public void pushDown()
    {
        //Debug.Log("OnPressed Invoked Child");
        sound.Stop();
        sound.clip = down;
        sound.Play();
    }

    public void pushUp()
    {
        sound.Stop();
        sound.clip = up;
        sound.Play();
        RightObjects();
    }

}
