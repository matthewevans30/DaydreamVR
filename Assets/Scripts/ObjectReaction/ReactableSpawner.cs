using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.XR.Interaction.Toolkit;

public class ReactableSpawner : MonoBehaviour
{
    public List<GameObject> reactors = new List<GameObject> ();
    public int spawnMax;
    int spawnCount = 0;
    public float forceScale;

    List<GameObject> spawns = new List<GameObject>();

    private void Start()
    {
        AudioAnalysis.audiosource = GetComponent<AudioSource>();
        AudioParameterDriver.rotSpeed = 20;
        AudioParameterDriver.startScale = new Vector3(1.5f, 1.5f, 1.5f);
        foreach(GameObject shape in reactors)
        {
            shape.GetComponent<Rigidbody>().drag = 0;
            shape.GetComponent<XRGrabInteractable>().enabled = false;
            
        }
        StartCoroutine(SpawnReactables());
    }

    public IEnumerator SpawnReactables()
    {
        while(spawnCount < spawnMax)
        {
            
            SpawnNewReactor();
            
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        foreach(GameObject spawned in spawns.ToList())
        {
            if (CheckBounds(spawned.transform.localPosition))
            {
                spawns.Remove(spawned);
                AudioParameterDriver.reactorList.Remove(spawned);
                Destroy(spawned);
                SpawnNewReactor();
            }
        }
    }

    bool CheckBounds(Vector3 position)
    {
        if (position.x > 12 || position.x < -12)
            return true;
        if (position.y > 12 || position.y < -3)
            return true;
        if (position.z > 12 || position.z < -12)
            return true;

        return false;
    }

    Vector3 GetRandomLocation()
    {
        float x = Random.Range(-11, 11);
        float y = Random.Range(0, 10);
        float z = Random.Range(-10, 10);

        return new Vector3(x, y, z);    
    }

    void SpawnNewReactor()
    {
        Vector3 force = Random.insideUnitSphere;
        force.Normalize();
        Vector3 location = GetRandomLocation();
        GameObject spawned = Instantiate(reactors[spawnCount % 3], location, Quaternion.Euler(Vector3.up));
        spawned.transform.localScale = AudioParameterDriver.startScale;
        spawned.GetComponent<Rigidbody>().AddForce(force * forceScale);
        spawned.GetComponent<AudioParameterDriver>().enabled = true;
        spawnCount++;
        spawns.Add(spawned);
    }
}
