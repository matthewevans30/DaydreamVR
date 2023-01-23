using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashLightsButton : ButtonVR
{
    AudioSource sound;
    public AudioClip down;
    public AudioClip up;
    bool lightsEnabled;
    public TextMeshPro textMesh;

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

    public void EnableLightFlash()
    {
        AudioParameterDriver.lightFlashEnabled = true;
        textMesh.text = "Disable\nLights";
    }

    public void DisableLightFlash()
    {
        AudioParameterDriver.lightFlashEnabled = false;
        textMesh.text = "Enable\nLights";
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
        if (!lightsEnabled)
        {
            EnableLightFlash();
            lightsEnabled = true;
        }
        else
        {
            DisableLightFlash();
            lightsEnabled = false;
        }
    }
}
