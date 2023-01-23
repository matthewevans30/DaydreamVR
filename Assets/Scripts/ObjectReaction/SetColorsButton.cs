using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetColorsButton : ButtonVR
{
    AudioSource sound;
    public AudioClip down;
    public AudioClip up;
    int clickCount;

    public ColorManager colorManager;
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

    public void ColorSet()
    {
        switch(clickCount % 3)
        {
            case 0:
                SetColors();
                break;
            case 1:
                SetColorsRandom();
                break;
            case 2:
                SetNoColors();
                break;
        }
    }

    public void SetColors()
    {
        Color newColor = colorManager.GetComponent<Light>().color;
        

        foreach (GameObject reactor in AudioParameterDriver.reactorList)
        {
            reactor.GetComponent<AudioParameterDriver>().matColor = newColor;
        }

        string text = "Set Random\nColors";
        textMesh.text = text;
        
    }

    public void SetColorsRandom()
    {
        foreach (GameObject reactor in AudioParameterDriver.reactorList)
        {
            reactor.GetComponent<AudioParameterDriver>().matColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
        }
        string text = "Disable\nColors";
        textMesh.text = text;
    }

    public void SetNoColors()
    {
        Color newColor = Color.black;
        foreach(GameObject reactor in AudioParameterDriver.reactorList)
        {
            reactor.GetComponent<AudioParameterDriver>().matColor = newColor;
        }
        string text = "Set Uniform\nColors";
        textMesh.text = text;
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
        ColorSet();
        clickCount++;
    }
}
