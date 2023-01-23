using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    public float bias;                      //how large spectrum value needs to be in order to trigger
    public float timeStep;                  //interval between each beat
    public float timeToBeat;                //how much time before viz is over
    public float restSmoothSpeed;            //how fast object goes to rest after beat

    private float m_previousAudioValue;     
    private float m_audioValue;             //allows us to determine if value went above or below bias during current frame
    private float m_timer;                  //keep track of timestep variable

    protected bool m_isBeat;                //keeps track of whether or not sync object is currently in a beat state

    public virtual void OnBeat()
    {
        Debug.Log("beat");
        m_timer = 0;
        m_isBeat = true;
    }

    public virtual void OnUpdate()
    {
        m_previousAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.spectrumValue;

        if(m_previousAudioValue > bias &&
            m_audioValue <= bias)
        {
            if (m_timer > timeStep)
                OnBeat();
        }

        if(m_previousAudioValue <= bias && 
            m_audioValue > bias)
        {
            if (m_timer > timeStep)
                OnBeat();
        }
        m_timer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
}
