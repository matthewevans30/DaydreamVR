using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
    public GameObject[] obj;
    GameObject currObject;
    GameObject spawnedObject;
    public float rotationAmount;
    public float rotationOffset;

    private void Start()
    {
        AudioParameterDriver.active = true;
        AudioParameterDriver.reactorList = new List<GameObject>();
        foreach(GameObject reactable in obj)
        {
            reactable.GetComponent<Rigidbody>().drag = 2;
        }
        currObject = obj[1];
        spawnedObject = Instantiate(currObject, gameObject.transform);
        setCollisionLayer("ObjectSelect");
    }

    public void FixedUpdate()
    {
        spawnedObject.transform.Rotate(0,  Time.deltaTime * rotationAmount, 0);
        
    }

    public void updateCurrObject(int index)
    {
        if(index < obj.Length)
        {
            rotationOffset = spawnedObject.transform.localRotation.eulerAngles.y;
            Destroy(spawnedObject);
            currObject = obj[index];
            spawnedObject = Instantiate(currObject, gameObject.transform);
            spawnedObject.transform.Rotate(0, rotationOffset, 0);
            setCollisionLayer("ObjectSelect");
            Debug.Log("New Object Spawned");
        }
    }

    public void ReplaceGrabbed()
    {
        spawnedObject.tag = "Untagged";

        rotationOffset = spawnedObject.transform.localRotation.eulerAngles.y;
        spawnedObject = Instantiate(currObject, gameObject.transform);
        spawnedObject.transform.Rotate(0, rotationOffset, 0);
        setCollisionLayer("ObjectSelect");
        Debug.Log("Object Replaced");
    }

    public void setCollisionLayer(string layer)
    {
        int ObjectSelectLayer = LayerMask.NameToLayer(layer);
        spawnedObject.transform.GetChild(0).gameObject.layer = ObjectSelectLayer;
    }


}
