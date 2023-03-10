using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public int scale;
    public float[] m_audioSpectrum;                             // array to hold spectrum data
    public static float spectrumValue { get; private set; }     //value to be retrieved from spectrum
    public int listenIndex;

    float count;

    // Start is called before the first frame update
    void Start()
    {
        m_audioSpectrum = new float[128];
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if(m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[listenIndex] * 100; 
        }

        count++;
        Debug.Log(count);
    }
}
