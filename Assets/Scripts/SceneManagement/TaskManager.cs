using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public AudioSource audioSource;
   [SerializeField] LightFlickerScript flicker;
    public int flickerDelaySeconds;
    int choreCount;
    public GameObject bed;
    public TextMeshPro[] taskList;
    public TextMeshPro newText;
    bool musicPlayed;
    bool trashCanFilled;
    bool rewritten;

    public void Start()
    {
        StartCoroutine("FlickerDelay");
        
    }

    public void taskComplete(int taskNum)
    {
        Debug.Log("TaskComplete");
        if(taskNum < 4)
        {
            audioSource.Play();
        }

        taskList[taskNum-1].SetText("<s>" + taskList[taskNum-1].text + "</s>");

        switch (taskNum)
        {
            case 1:
                if (!trashCanFilled)
                {
                    trashCanFilled = true;
                    choreCount++;
                    StartCoroutine("ShortFlickerDelay");
                }
                break;
            case 2:
                choreCount++;
                break;
            case 3:
                choreCount++;
               break;   
            case 4:
                if (!musicPlayed)
                {
                    musicPlayed = true;
                    choreCount++;
                    audioSource.Play();
                }
                break;  
            default:
                break;
        }
        tasksCompleteCheck();
    }

    public IEnumerator ShortFlickerDelay()
    {
        yield return new WaitForSeconds(1);

        if (!flicker.started)
        {
            flicker.started = true;
            flicker.flickerStarter();
        }
    }

    public IEnumerator FlickerDelay()
    {
        yield return new WaitForSeconds(flickerDelaySeconds);
        if (!flicker.started)
        {
            flicker.started = true;
            flicker.flickerStarter();
        }
    }

    public void tasksCompleteCheck()
    {
        if (choreCount < 4)
            return;

        if(choreCount == 4 && !rewritten)
            StartCoroutine("RewriteWhiteBoard");
    }

    public IEnumerator RewriteWhiteBoard()
    {
        rewritten = true;
        float alpha = 1;
        Color currColor = taskList[0].color;
        while(alpha > 0)
        {
            foreach(TextMeshPro text in taskList)
            {
                alpha -= 0.01f;
                text.color = (currColor * alpha);
                yield return new WaitForFixedUpdate();
            }
        }
        Debug.Log("Writing New Text and Spawning trigger");
        yield return new WaitForSeconds(2);
        newText.color = currColor * 256;
        bed.GetComponent<BoxCollider>().enabled = true;
        yield return null;
    }
}
