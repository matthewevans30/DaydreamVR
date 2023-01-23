using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScaleControlSlider : SliderBase
{
    float sliderStart;
    float currPosition;
    float sliderLength = 0.15f;

    float hitScale;

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

                hitScale = currPosition - sliderStart;
                hitScale = (hitScale / (sliderLength * 2));

                hitScale = Map(0f, 4f, 0f, 1f, hitScale);
                Vector3 newHitScale = new Vector3(hitScale, hitScale, hitScale);    

                if(newHitScale.x >= AudioParameterDriver.startScale.x)
                {
                    Debug.Log(newHitScale);
                    AudioParameterDriver.hitScale = newHitScale
;               }
                string text = "Scale Jump Control\n\nCurrent Value = " + newHitScale.x.ToString("F2");
                UpdateCanvas(text);

            }
        }
    }
}
