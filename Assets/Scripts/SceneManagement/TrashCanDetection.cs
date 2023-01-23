using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrashCanDetection : MonoBehaviour
{
    public float glowTime = 2f;
    private Material material;

    float ballCount;

    public XRBaseController RightController, LeftController;

    public TaskManager manager;

    public void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PaperBall")
        {
            ballCount++;
            other.gameObject.tag = "Untagged";
            Debug.Log("EnteredBin");
            RightController.SendHapticImpulse(1f, 0.5f);
            LeftController.SendHapticImpulse(1f, 0.5f);

            Debug.Log(ballCount);

            StopCoroutine("EmmissiveFlash");
            StartCoroutine("EmmissiveFlash");

            

        }
    }

    private IEnumerator EmmissiveFlash()
    {
        float currentVal = 0;
        float timer = 0;

        while(currentVal < 1)
        {
            currentVal = Mathf.Lerp(0, 1, timer / (glowTime / 2));
            material.SetColor("_EmissionColor", new Color(currentVal, currentVal, currentVal));
            timer += Time.deltaTime;
            //Debug.Log(currentVal);
            yield return null;
        }

        timer = 0;

        while(currentVal > 0)
        {
            currentVal = Mathf.Lerp(1, 0, timer / (glowTime / 2));
            material.SetColor("_EmissionColor", new Color(currentVal, currentVal, currentVal));
            timer += Time.deltaTime;
            //Debug.Log(currentVal);
            yield return null;
        }

        if (ballCount >= 4)
        {
            Debug.Log("FlickerStarted");
            manager.taskComplete(1);
        }
    }
}
