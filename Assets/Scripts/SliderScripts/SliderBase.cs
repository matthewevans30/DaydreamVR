using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderBase : MonoBehaviour
{
    public bool selected;
    public Canvas canvas;

    private void Start()
    {
        
    }

    public virtual void FixedUpdate()
    {
    
    }

    public virtual void OnSelectEnter()
    {
        selected = true;
    }

    public virtual void OnSelectExit()
    {
        selected = false;
    }

    public virtual void UpdateCanvas(string text)
    {
        canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }

    public float Map(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
}
