using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorSliderActivation : MonoBehaviour
{
    public ColorManager colorManager;
    public void OnSelectEnter()
    {
        colorManager.sliderSelected = true;
    }

    public void OnSelectExit()
    {
        colorManager.sliderSelected=false;
    }
}
