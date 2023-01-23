using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class EnableImpulseButton : ButtonVR
{
    AudioSource sound;
    public AudioClip down;
    public AudioClip up;
    bool impulseEnabled;
    public TextMeshPro textMesh;
    int count;

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

    public void ChangeButton()
    {
        switch (count % 3)
        {
            case 0:
                EnableImpulse();
                break;
            case 1:
                EnableTeleport();
                break;
            case 2:
                NoImpulse();
                break;
        }
    }

    public void EnableImpulse()
    {
        AudioParameterDriver.enableImpulse = true;
        textMesh.text = "Enable\nTeleport";
    }

    public void EnableTeleport()
    {
        AudioParameterDriver.enableImpulse = false;
        AudioParameterDriver.enableTeleport = true;
        textMesh.text = "Reset";
    }

    public void NoImpulse()
    {
        AudioParameterDriver.enableTeleport = false;
        AudioParameterDriver.enableImpulse = false;
        textMesh.text = "Enable\nMovement";
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
        ChangeButton();
        count++;
    }
}
