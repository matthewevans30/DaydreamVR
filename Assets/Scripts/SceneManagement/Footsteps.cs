using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public CharacterController cc;
    public AudioSource footsteps;
    bool delayOver;

    private void Start()
    {
        StartCoroutine(FootstepDelay());
    }
    // Update is called once per frame
    void Update()
    {
        if(cc.isGrounded && cc.velocity.magnitude >= 1.8f && !footsteps.isPlaying && delayOver)
        {
            footsteps.volume = Random.Range(0.5f, 0.75f);
            footsteps.pitch = Random.Range(0.7f, 0.9f);
            footsteps.Play();   
        }
    }

    public IEnumerator FootstepDelay()
    {
        yield return new WaitForSeconds(1);
        delayOver = true;
    }
}
