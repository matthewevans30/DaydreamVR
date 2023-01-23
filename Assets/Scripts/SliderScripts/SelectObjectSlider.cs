using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectObjectSlider : SliderBase
{
    public ObjectSelect objSelect;

    public float sliderLength = 0.15f;
    public float startPos;
    public int currIndex;

    // Start is called before the first frame update
    void Start()
    {
        currIndex = 1;
        float middlePos = this.transform.position.z;
        startPos = middlePos - (sliderLength);
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if (selected)
        {
            float currPos = this.transform.position.z;

            currPos = currPos - startPos;

            int newIndex = 0;
            //Debug.Log(currPos);

            if (currPos <= 0.1f)
            {
                newIndex = 0;
            }
            else if (currPos <= 0.2f)
            {
                newIndex = 1;
            }
            else
            {
                newIndex = 2;
            }

            if (newIndex != currIndex)
            {
                objSelect.updateCurrObject(newIndex);
                currIndex = newIndex;
            }
        }
    }
}
