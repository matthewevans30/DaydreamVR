using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomsButton : ButtonVR
{
    AudioSource sound;
    public AudioClip down;
    public AudioClip up;

    public int objectsToSpawn;
    public GameObject[] objects;
    int objectPos;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        sound = GetComponent<AudioSource>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void SpawnObjects()
    {
        for(int i = 0; i < objectsToSpawn; i++)
        {
            float x = Random.Range(1.28f, 6.75f);
            float y = Random.Range(23.7f, 25f);
            float z = Random.Range(-2.8f, 2.4f);
            Vector3 newPos = new Vector3(x, y, z);
            Vector3 rotationVector = new Vector3(0, 0, 0);
            Quaternion newRot = Quaternion.Euler(rotationVector);

            int index = objectPos % 3;
            GameObject newObject = Instantiate(objects[index], newPos, newRot);
            newObject.GetComponent<AudioParameterDriver>().matColor = ColorManager.currentColor;
            AudioParameterDriver.reactorList.Add(newObject);
            newObject.GetComponent<AudioParameterDriver>().enabled = true;
            Debug.Log("New Object Spawned");

            objectPos++;

        }
    }

    public void pushDown()
    {
        //Debug.Log("OnPressed Invoked Child");
        sound.Stop();
        sound.clip = down;
        sound.Play();
    }

    public void pushUp()
    {
        sound.Stop();
        sound.clip = up;
        sound.Play();
        SpawnObjects();
    }
}
