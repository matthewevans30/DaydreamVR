using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdControl : SliderBase
{
    float sliderStart;
    float currPosition;
    float sliderLength = 0.15f;

    float threshold;

    // Start is called before the first frame update
    void Start()
    {
        sliderStart = transform.localPosition.z - (sliderLength);
        currPosition = transform.localPosition.z;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if (selected)
        {
            if (currPosition != transform.localPosition.z)
            {
                currPosition = transform.localPosition.z;

                threshold = currPosition - sliderStart;
                threshold = (threshold / (sliderLength * 2));


                AudioParameterDriver.threshold = threshold;
                string text = "Threshold Control\n\nCurrent Value = " + threshold.ToString("F2");
                base.UpdateCanvas(text);
            }
        }
    }
}
