using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RotateControlSlider : SliderBase
{
    float sliderStart;
    float currPosition;
    float sliderLength = 0.15f;

    public float maxSpeed;

    float rotationSpeed;

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

                rotationSpeed = currPosition - sliderStart;
                rotationSpeed = (rotationSpeed / (sliderLength * 2));

                rotationSpeed = Map(-maxSpeed, maxSpeed, 0f, 1f, rotationSpeed);

                AudioParameterDriver.rotSpeed = rotationSpeed;
                string text = "Rotation Control \n\nCurrent Rotation = \n" + Mathf.Round(rotationSpeed);
                base.UpdateCanvas(text);
            }
        }
    }


}
