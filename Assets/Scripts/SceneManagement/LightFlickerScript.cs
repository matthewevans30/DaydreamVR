using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.XR.Interaction.Toolkit;

public class LightFlickerScript : MonoBehaviour
{
    public GameObject light1, light2, light3;
    public GameObject[] lights;
    int lightNum;
    public bool started;
    public bool finished;
    public TaskManager manager;

    public void Start()
    {
        lights = new GameObject[3];
        lights[0] = light1;
        lights[1] = light2; 
        lights[2] = light3; 
    }

    public void flickerStarter()
    {
        StartCoroutine("startFlicker");
    }

    public IEnumerator startFlicker()
    {
        Debug.Log("insideFlickerEnum");
        while(lightNum < lights.Length)
        {
            bool on = true;
            Light bulb = lights[lightNum].transform.GetChild(0).GetComponent<Light>();
            Material lampShadeMat = lights[lightNum].GetComponent<Renderer>().material;
            lights[lightNum].GetComponent <XRSocketInteractor>().enabled = true;
            Color emissionColor = lampShadeMat.GetColor("_EmissionColor");
            float originalIntensity = bulb.intensity;

            while (lights[lightNum].tag == "FlickeringLight")
            {
                bulb.intensity = on ? originalIntensity : 0;
                float emissionIntensity = on ? 1 : 0;
                on = !on;
                lampShadeMat.SetColor("_EmissionColor", (emissionColor * emissionIntensity));
                float waitTime = Random.Range(0f, 0.7f);
                Debug.Log(bulb.intensity);
                yield return new WaitForSeconds(waitTime);
                
            }
            lightNum++;
        }
        manager.taskComplete(2);
    }
}
