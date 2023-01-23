using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiHatReaction : DrumReaction
{
    public override void TriggerReaction()
    {
        foreach(GameObject reactor in AudioParameterDriver.reactorList)
        {
            reactor.GetComponent<AudioParameterDriver>().MatPulse();
            reactor.GetComponent<AudioParameterDriver>().LightPulse();
        }

        if (AudioParameterDriver.lightFlashEnabled)
        {
            foreach (GameObject lamp in LightList.lightList)
                lamp.GetComponent<AudioParameterDriver>().LightPulse();
        }
    }
}
