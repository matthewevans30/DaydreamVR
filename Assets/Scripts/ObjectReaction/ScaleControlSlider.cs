using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleControlSlider : SliderBase
{
    float sliderStart;
    float currPosition;
    float sliderLength = 0.15f;

    float scale;

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

                scale = currPosition - sliderStart;
                scale = (scale / (sliderLength * 2));

                scale = Map(0f, 2f, 0f, 1f, scale);

                Vector3 newScale = new Vector3(scale, scale, scale);

                foreach(GameObject reactor in AudioParameterDriver.reactorList)
                {
                    reactor.GetComponent<AudioParameterDriver>().UpdateScale(newScale);
                }

                string text = "Scale Control\n\nCurrent Scale = " + newScale.x.ToString("F2");
                UpdateCanvas(text);
            }
        }
    }
}
