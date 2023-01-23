using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncScale : AudioSyncer
{
    public Vector3 beatScale;
    public Vector3 restScale;
    public List<GameObject> reactors = new List<GameObject>();

    public override void OnUpdate()
    {
        //Debug.Log("Helooo");
        base.OnUpdate();

        if (m_isBeat) return;

        foreach (GameObject reactor in reactors)
        {
            reactor.transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothSpeed * Time.deltaTime);
        }
    }

    public override void OnBeat()
    {
        base.OnBeat();
        Debug.Log("insideOnBeat");
        StopCoroutine("MoveToScale");

        foreach(GameObject reactor in reactors)
        {
            StartCoroutine("MoveToScale", reactor);
        }
        
    }

    private IEnumerator MoveToScale(GameObject reactor)
    {
        
        if (reactors.Count > 0)
        {
            Vector3 _curr = reactors[0].transform.localScale;
            Vector3 _initial = _curr;
            float _timer = 0;                       //keeps track of how far we are between initial and target scale


            Debug.Log("Beat Coroutine");

            while (_curr != beatScale)
            {
                
                _curr = Vector3.Lerp(_initial, beatScale, _timer / timeToBeat);
                _timer += Time.deltaTime;

                reactor.transform.localScale = _curr;

                    
                
                yield return null;
            }
            m_isBeat = false;
        }
        
    }
}
