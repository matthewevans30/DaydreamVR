using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumReaction : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource source;

    public bool useVelocity = true;
    public float minVelocity = 0;
    public float maxVelocity = 2;

    public bool randomizePitch = true;
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;

    public HapticController haptics;
    
    //public DrumPractice drumPractice;

    // Start is called before the first frame update
    public virtual void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftStick") || other.CompareTag("RightStick"))
        {
            VelocityEstimator estimator = other.GetComponent<VelocityEstimator>();

            float v = estimator.GetVelocityEstimate().magnitude;
            float volume = Mathf.InverseLerp(minVelocity, maxVelocity, v);

            if (randomizePitch)
                source.pitch = Random.Range(minPitch, maxPitch);

            if (other.CompareTag("LeftStick"))
            {
                haptics.SendHaptics(true, false, 0.5f, 0.1f);
            }
            else
            {
                haptics.SendHaptics(false, true, 0.5f, 0.1f);
            }

            source.PlayOneShot(clip, volume);
            TriggerReaction();

        }
    }

    public virtual void TriggerReaction()
    {

    }
}
