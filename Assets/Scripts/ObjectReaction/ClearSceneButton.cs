using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSceneButton : ButtonVR
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

    public void DestroyObjects()
    {
        foreach (GameObject reactor in AudioParameterDriver.reactorList)
        {
            Destroy(reactor);
        }
        AudioParameterDriver.reactorList.Clear();
        Debug.Log("Objects Destroyed");
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
        DestroyObjects();
    }
}
