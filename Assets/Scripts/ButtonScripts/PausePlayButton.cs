using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PausePlayButton : ButtonVR
{
    AudioSource sound;
    public AudioClip down;
    public AudioClip up;

    public static bool armIsHeld;
    public MusicPlayerScene2 player;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    //override for this to make sure arm is not held - avoids accidentally pressing buttons when moving
    //plattenspieler arm
    public override void OnTriggerEnter(Collider other)
    {
        if (!armIsHeld && (other.CompareTag("RightHand") || other.CompareTag("LeftHand")))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.SetActive(true);
            active = true;
        }

    }

    public void pushDown()
    {
        //Debug.Log("OnPressed Invoked Child");
        sound.Stop();
        sound.clip = down;
        sound.Play();
    }

    public void pause()
    {

        sound.Stop();
        sound.clip = up;
        sound.Play();
        player.stopSpinning();
    }

    public void play()
    {
        sound.Stop();
        sound.clip = up;
        sound.Play();
        player.startSpinning();
    }


}
