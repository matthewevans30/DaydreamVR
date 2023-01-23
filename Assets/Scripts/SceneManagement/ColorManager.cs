using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Material mainMaterial;
    public GameObject RedControl, GreenControl, BlueControl;
    float redStart, greenStart, blueStart;
    float redEnd, greenEnd, blueEnd;
    public float slideLength = -0.2f;
    public Light light;
    public static Color currentColor;

    public static GameObject sphereInWorld;

    public bool sliderSelected;

    private void Start()
    {
        redStart = RedControl.transform.position.x;
        greenStart = GreenControl.transform.position.x;
        blueStart = BlueControl.transform.position.x;

        redEnd = redStart + slideLength;
        greenEnd = greenStart + slideLength;
        blueEnd = blueStart + slideLength;

        mainMaterial.SetColor("_Color", new Color(0,0,0));
        mainMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));
        currentColor = Color.black;
    }

    private void FixedUpdate()
    {
        if (sliderSelected)
        {
            float newRed = map(RedControl);
            float newGreen = map(GreenControl);
            float newBlue = map(BlueControl);

            currentColor = new Color(newRed, newGreen, newBlue);

            mainMaterial.SetColor("_EmissionColor", currentColor);

            light.color = currentColor; 

            if(sphereInWorld != null)
            {
                sphereInWorld.GetComponent<Light>().color = currentColor;    
            }
        }
    }

    private float map(GameObject slider)
    {

        float colorAmt = slider.transform.position.x - redStart;
        colorAmt = colorAmt / slideLength;
        //colorAmt *= 255;

        return colorAmt;
    }


}
