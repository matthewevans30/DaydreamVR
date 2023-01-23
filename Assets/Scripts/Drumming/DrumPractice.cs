using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumPractice : MonoBehaviour
{
    public int hitCount;
    int hitTarget;
    public HapticController haptics;
    public TaskManager manager;

    // Start is called before the first frame update
    void Start()
    {
        hitTarget = Random.Range(50, 70);
    }

    public void checkCompletion()
    {
        if(hitCount == hitTarget)
        {
            haptics.SendHaptics(true, true, 1, 2);
            manager.taskComplete(3);
        }
    }
}
