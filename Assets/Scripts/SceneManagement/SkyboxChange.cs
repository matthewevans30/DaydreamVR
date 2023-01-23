using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChange : MonoBehaviour
{
    public Material skybox2;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skybox2;
        Debug.Log("Skybox Changed");
    }
}
