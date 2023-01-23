using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightList : MonoBehaviour
{
    public static List<GameObject> lightList = new List<GameObject> ();

    private void Awake()
    {
        lightList.AddRange(GameObject.FindGameObjectsWithTag("FlickeringLight"));
        Debug.Log(lightList.Count);
    }
}
