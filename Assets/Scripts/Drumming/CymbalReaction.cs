using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CymbalReaction : DrumReaction
{
    public override void TriggerReaction()
    {
        foreach (GameObject reactor in AudioParameterDriver.reactorList)
        {
            reactor.GetComponent<AudioParameterDriver>().ForceImpulse();
            reactor.GetComponent<AudioParameterDriver>().Teleport();
            reactor.GetComponent<AudioParameterDriver>().MatPulse();
        }

        if (AudioParameterDriver.lightFlashEnabled)
        {
            foreach (GameObject lamp in LightList.lightList)
                lamp.GetComponent<AudioParameterDriver>().LightPulse();
        }
    }
}
