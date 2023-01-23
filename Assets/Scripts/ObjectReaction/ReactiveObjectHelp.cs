using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReactiveObjectHelp : MonoBehaviour
{

    ObjectSelect select;
    bool replaced;
    float intensity;
    public Color newColor;
    

    private void Awake()
    {
        select = FindObjectOfType<ObjectSelect>(); 
    }

    public void OnSelectEnterFirst()
    {
        if (!replaced)
        {
            int GrabbablesLayer = LayerMask.NameToLayer("Grabbables");
            transform.GetChild(0).gameObject.layer = GrabbablesLayer;
            transform.GetChild(0).tag = "NewReactive";
            GetComponent<AudioParameterDriver>().enabled = true;
            select = FindObjectOfType<ObjectSelect>();
            select.ReplaceGrabbed();
            replaced = true;
        }

        GetComponent<AudioParameterDriver>().enabled = false;
    }

    public void OnSelectExit()
    {
        GetComponent<AudioParameterDriver>().enabled = true;   
    }



    public void Pulse()
    {
        StartCoroutine(PulseMaterial());
    }

    //public IEnumerator PulseGlow()
    //{

    //    while(intensity > 0)
    //    {
    //        intensity -= 0.2f;
    //        glow.intensity = intensity;
    //        yield return null;  
    //    }
    //    yield return null;  
    //}

    public IEnumerator PulseMaterial()
    {
        float lerpSpeed = 2f;
        Material mat = transform.GetChild(0).GetComponent<Renderer>().material;
        Color baseColor = Color.black;
        Color currColor = baseColor;
        Color targetColor = newColor;
        float t = 0;

        while(t < 1)
        {
            //glow.intensity = Mathf.Lerp(0f, 1f, t);
            currColor = Color.Lerp(baseColor, targetColor, t);
            mat.SetColor("_EmissionColor", currColor);
            t += (lerpSpeed * Time.deltaTime);
            yield return null;
        }
        //Debug.Log("FinishedFirstLoop");

        t = 0; 

        while(t < 1)
        {
            //glow.intensity = Mathf.Lerp(1f, 0f, t);
            mat.SetColor("_EmissionColor", Color.Lerp(targetColor, baseColor, t));
            t += (lerpSpeed * Time.deltaTime);
            yield return null;
        }
        mat.SetColor("_EmissionColor", newColor * 0);
        //glow.intensity = 0;


        yield return null;
    }
}
