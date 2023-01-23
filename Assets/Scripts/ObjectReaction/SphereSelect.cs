using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SphereSelect : MonoBehaviour
{
    public GameObject sphereToSpawn;
    GameObject sphereFromSpawn;
    public Vector3 originalPosition;

    private void Start()
    {
    }

    public void ReplaceGrabbed()
    {
        sphereFromSpawn = Instantiate(sphereToSpawn);
        sphereFromSpawn.transform.position = originalPosition;
        Debug.Log("SphereSpawned");

    }

    

    
}
