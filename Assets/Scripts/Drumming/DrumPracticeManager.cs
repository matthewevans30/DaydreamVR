using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumPracticeManager : MonoBehaviour
{
    int hitCount;
    int hitsRequired;

    void Start()
    {
        hitsRequired = Random.Range(20, 30);
    }

    public void DrumPracticeComplete()
    {
        if(hitCount >= hitsRequired)
        {
            
        }
    }
}
