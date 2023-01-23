using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlider : MonoBehaviour
{
    public ObjectSelect objSelect;

    float sliderLength = 0.15f;
    float startPos;
    int currIndex;
    bool Selected;

    private void Start()
    {
        currIndex = 1;
        float middlePos = this.transform.position.z;
        startPos = middlePos - (sliderLength);

    }

    private void FixedUpdate()
    {
        
    }

    public void OnSelectEnter()
    {
        Selected = true;
    }

    public void OnSelectExit()
    {
        Selected = false;
    }
}
