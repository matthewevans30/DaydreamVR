using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioParameterDriver : MonoBehaviour
{

    public static Vector3 startScale;      //initial scale
    public static Vector3 hitScale;        //scale to jump to on hit
    public bool useBuffer;
    bool scalePrimed;
    bool lightPrimed;
    bool impulsePrimed;
    bool teleportPrimed;
    float impulseWait;
    float teleportWait;

    public float scaleMultiplier;

    public static float threshold;         //determines what qualifies as a hit
    public static float recoverySpeed;   //determines how quickly obj will return to startScale

    Material mat;
    [ColorUsage(true, true)]
    public Color matColor;
    float matAlpha;
    public static float glowDrop;
    public static bool active;

    public static List<GameObject> reactorList = new List<GameObject>();

    public bool isLamp;
    public static bool lightFlashEnabled;
    Light light;
    public Color lampShadeColor;
    public static bool updateMaterialRequired;

    public static float rotSpeed;
    public static float objectScale;
    public float impulseScale;
    public float teleportLength;

    public static bool enableImpulse;
    public static bool enableTeleport;

    private void Start()
    {
        scalePrimed = true;
        recoverySpeed = 0.9f;
        threshold = 0.5f;
        
        if (!isLamp)
        {
            startScale = transform.localScale;
            hitScale = startScale * 2;
            mat = transform.GetChild(0).GetComponent<Renderer>().material;
            reactorList.Add(gameObject);
        }

        if (isLamp)
        {
            light = transform.GetChild(0).GetComponent<Light>();
            mat = GetComponent<Renderer>().material;
            lampShadeColor = mat.GetColor("_EmissionColor");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            float bandValue = AudioAnalysis.bandBuffer[AudioAnalysis.FocusBand];        //normalized between 0 and 1

            if (bandValue > threshold)
            {
                sendHits();
            }
            else
            {
                SendEmpties();
            }
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
    }

    //hit detected, update components
    void sendHits()
    {
        if (!isLamp)
        {
            MatPulse();
            ForceImpulse();
            Teleport();
            ScaleHit();
        }else if(isLamp && lightFlashEnabled)
        {
            MatPulse();
            LightPulse();
        }

    }

    //no hit detected, decrease components toward original state
    void SendEmpties()
    {
        if (!isLamp)
        {
            ScaleDecrease();
            MatDecrease();
            ForceDelay();
            TeleportDelay();
        }else if(isLamp && lightFlashEnabled)
        {
            LightDecrease();
        }
    }

    //bring material up to max alpha on beat
    public void MatPulse()
    {
        if (scalePrimed)
        {
            matAlpha = 1f;
            mat.SetColor("_EmissionColor", matColor * matAlpha);
        }
    }

    //bring light up to max alpha on beat 
    public void LightPulse()
    {
        if (lightPrimed)
        {
            light.intensity = 1f;
            mat.SetColor("_EmissionColor", lampShadeColor * 1);
            lightPrimed = false;
        }
    }

    //bring object up to hitscale on beat
    public void ScaleHit()
    {
        if (scalePrimed)
        {
            transform.localScale = hitScale;
            scalePrimed = false;
        }
    }

    //add force on beat
    public void ForceImpulse()
    {
        if (enableImpulse)
        {
            if (impulsePrimed)
            {
                Vector3 direction = Random.insideUnitSphere;

                direction.y = 0;
                Vector3.Normalize(direction);
                direction *= impulseScale;
                GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
                Debug.Log("ImpulseAdded");
                impulseWait = 3f;
                impulsePrimed = false;
            }
        }
    }

    //teleport objects on beat
    public void Teleport()
    {
        if (enableTeleport)
        {
            if (teleportPrimed)
            {
                
                float x = Random.Range(1.28f, 6.75f);
                float y = transform.position.y;
                float z = Random.Range(-2.8f, 2.4f);
                Vector3 newPosition = new Vector3(x, y, z);
                transform.position = newPosition;
                //Debug.Log("Teleported");
                teleportWait = 1f;
                teleportPrimed = false;
            }
        }
    }

    //teleport cooldown
    void TeleportDelay()
    {
        if (enableTeleport)
        {
            teleportWait *= recoverySpeed;
            if (teleportWait < 0.01f)
                teleportPrimed = true;
        }
    }

    //add force cooldown
    void ForceDelay()
    {
        if (enableImpulse)
        {
            impulseWait *= recoverySpeed;
            if (impulseWait < 0.01f)
                impulsePrimed = true;
        }
    }

    //bring object back to original scale
    void ScaleDecrease()
    {
        if (Vector3.Distance(transform.localScale, startScale) > 0.2f)
        {
            Vector3 newScale = transform.localScale * recoverySpeed;
            transform.localScale = newScale;
        }
        else
        {
            scalePrimed = true;
        }
    }

    //bring mat back to 0
    void MatDecrease()
    {
        if(matAlpha >= 0.001)
        {
            matAlpha *= recoverySpeed;
            mat.SetColor("_EmissionColor", matColor * matAlpha);
        }
        
        
    }

    //bring light back to 0
    void LightDecrease()
    {
        if(light.intensity > 0.01f)
        {
            //matAlpha *= recoverySpeed;
            light.intensity *= recoverySpeed;
            mat.SetColor("_EmissionColor", lampShadeColor * light.intensity);
        }
        else
        {
            light.intensity = 0f;
            mat.SetColor("_EmissionColor", lampShadeColor * 0);
            lightPrimed = true;
        }
    }

    //used to update hitScale via slider
    public void UpdateScale(Vector3 newScale)
    {
        startScale = newScale;
        if(startScale.magnitude > hitScale.magnitude)
        {
            hitScale = startScale;
        }
        transform.localScale = newScale;

    }

    public static void UpdateMaterialRequired()
    {
        updateMaterialRequired = true;
    }

}
