using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SphereSelectHelp : MonoBehaviour
{
    SphereSelect select;
    bool replaced;

    public Vector3 targetScale;
    float scaleChange = 0.9f;
    float scaleChangeShrink = 0.9f;
    public Vector3 initialScale;
    float shrinkTime = 0.1f;
    float destroyTime = 0.65f;

    bool insideReactive;
    GameObject reactiveObject;
    
    public float fadeSpeed = 5f;
    public ColorManager colorManager;

    bool isHeld = false;

    private void Awake()
    {
        select = FindObjectOfType<SphereSelect>();
        initialScale = transform.localScale;
        colorManager = FindObjectOfType<ColorManager>();

    }

    public void OnSelectEnterFirst()
    {
        isHeld = true;

        if (ColorManager.sphereInWorld && 
            !GameObject.ReferenceEquals(ColorManager.sphereInWorld, gameObject))
        {
            Destroy(ColorManager.sphereInWorld);
        }

        ColorManager.sphereInWorld = gameObject;

        if (!replaced)
        {
            CopyLightToSphere();
            int GrabbablesLayer = LayerMask.NameToLayer("Grabbables");
            transform.gameObject.layer = GrabbablesLayer;
            select = FindObjectOfType<SphereSelect>();
            select.ReplaceGrabbed();
            replaced = true;
        }
    }

    public void OnSelectExit()
    {
        Debug.Log("SelectExited");

        if (insideReactive)
        {
            Debug.Log("DroppedInsideObject");
            MergeWithReactiveObject();

        }
        else
        {
            isHeld = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHeld && other.gameObject.CompareTag("NewReactive") && replaced)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            reactiveObject = other.transform.parent.gameObject;
            MergeWithReactiveObject();
        }else if(!isHeld && other.gameObject.CompareTag("ColorChangeTrigger") && replaced)
        {
            reactiveObject = other.transform.parent.gameObject;
            MergeWithLight();
        }
        else if (other.gameObject.CompareTag("NewReactive") && replaced)
        {
            insideReactive = true;
            reactiveObject = other.transform.parent.gameObject;
            StartCoroutine(ShrinkSphere());
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NewReactive") && replaced)
        {
            insideReactive = false;
            StartCoroutine(GrowSphere());
        }
    }

    public void MergeWithLight()
    {
        CopyLightToLamp();

        //use transform of light child for lerp function
        reactiveObject = reactiveObject.transform.GetChild(0).gameObject;
        Debug.Log(reactiveObject.ToString());   
        StartCoroutine(ShrinkToNothing());
    }

    public void MergeWithReactiveObject()
    {
        //CopyLightToWireframe();
        Color lightColor = GetComponent<Light>().color;
        reactiveObject.GetComponent<ReactiveObjectHelp>().newColor = lightColor;
        reactiveObject.GetComponent<AudioParameterDriver>().matColor = lightColor;
        StartCoroutine(ShrinkToNothing());
        reactiveObject.GetComponent<ReactiveObjectHelp>().Pulse();
    }

    public IEnumerator ShrinkSphere()
    {
        float timer = 0;
        Vector3 startScale = transform.localScale;

        while (timer < shrinkTime)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timer/shrinkTime);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public IEnumerator GrowSphere()
    {
        float timer = 0;
        Vector3 startScale = transform.localScale;

        while (timer < shrinkTime)
        {
            transform.localScale = Vector3.Lerp(startScale, initialScale, timer / shrinkTime);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public IEnumerator ShrinkToNothing()
    {
        float timer = 0;
        Vector3 startScale = transform.localScale;

        while(timer < destroyTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, reactiveObject.transform.position, timer);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        //float dist = transform.localscale.magnitude - 0.1f;
        //float t = 0;
        //vector3 initposition = transform.localposition;

        //while (transform.localscale.magnitude > 0.1f)
        //{
        //    transform.localposition = vector3.lerp(transform.localposition, reactiveobject.transform.position, t );
        //    transform.localscale *= scalechangeshrink;
        //    t = 1f - ((transform.localscale.magnitude - 0.1f) / dist);
        //    //debug.log("t = " + t);
        //    yield return null;
        //}

        Destroy(gameObject);
        yield return null;
    }

    public void CopyLightToLamp()
    {
        Color glowColor = GetComponent<Light>().color;
        reactiveObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", glowColor);
        reactiveObject.transform.GetChild(0).GetComponent<Light>().color = glowColor;
        reactiveObject.GetComponent<AudioParameterDriver>().lampShadeColor = glowColor;
    }

    public void CopyLightToWireframe()
    {
        Light sphereGlow = GetComponent<Light>();
        Light glow = reactiveObject.GetComponent<Light>();
        glow.color = sphereGlow.color;
        glow.intensity = sphereGlow.intensity;
        glow.range = sphereGlow.range;
        Debug.Log("LightCopied");
    }

    public void CopyLightToSphere()
    {
        Light lightToCopy = colorManager.GetComponent<Light>();
        Light copyTo = GetComponent<Light>();
        copyTo.color = lightToCopy.color;  
        copyTo.intensity = lightToCopy.intensity;
        copyTo.range = lightToCopy.range;
    }
}
