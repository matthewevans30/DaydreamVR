using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordInteraction : MonoBehaviour
{

    /*public GameObject RecordPrefab;
    public float offset = 0.5f;
    public float slideTime = 0.5f;
    bool animOccurred;
    MeshRenderer cover;

    public void onSelectEnter()
    {
        cover = gameObject.GetComponent<MeshRenderer>();
        Debug.Log("Selected");

        if (RecordPrefab)
        {
            RecordPrefab = Instantiate(RecordPrefab, this.transform.position, new Quaternion(0, -0.707106829f, 0.707106829f, 0), this.transform);
            Debug.Log("Record Instantiated");
            cover.enabled = false;
        }
    }

    public void onSelectExit()
    {
        cover.enabled = true;
    }

    public void onHoverEnter()
    {
        if(RecordPrefab != null && !animOccurred)
        {
            Debug.Log("onHoverEnter Record is not null");
            StopCoroutine("RecordSlide");
            StartCoroutine("RecordSlide");
        }
    }

    private IEnumerator RecordSlide()
    {
        Vector3 initial = RecordPrefab.transform.position;
        Vector3 target = new Vector3(initial[0] + offset, initial[1], initial[2]);
        Vector3 current = initial;
        float timer = 0;

        Debug.Log("CalledCoroutine");
        while (current != target)
        {
            Debug.Log("Inside While Loop");
            current = Vector3.Lerp(initial, target, timer / slideTime);
            timer += Time.deltaTime;
            RecordPrefab.transform.position = current;

            yield return null;
        }
        animOccurred = true;*/
    //}
}
