using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSpeedControl : SliderBase
{
    float sliderStart;
    float currPosition;
    float sliderLength = 0.15f;

    float recoverySpeed;

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

                recoverySpeed = currPosition - sliderStart;
                recoverySpeed = (recoverySpeed / (sliderLength * 2));

                float sliderDisplayValue = recoverySpeed;
                recoverySpeed = Map(0.99f, 0.7f, 0f, 1f, recoverySpeed);
                AudioParameterDriver.recoverySpeed = recoverySpeed;
                
                string text = "CoolDown Speed\n\nCurrent Value = " + sliderDisplayValue.ToString("F2");
                UpdateCanvas(text);
            }
        }
    }
}
