using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordSlider : MonoBehaviour
{
    public GameObject RecordPrefab;
    public float offset = 0.5f;
    public float slideTime = 0.5f;
    bool _animOccurred;
    bool rightSide;

    void Start()
    {
        XRBaseInteractable grabbable = GetComponent<XRBaseInteractable>();
        grabbable.activated.AddListener(SlideRecord);
    }

    //start recordSlide animation on activation
    public void SlideRecord(ActivateEventArgs arg)
    {
        if (!_animOccurred)
        {
            RecordPrefab = gameObject.transform.GetChild(2).gameObject;
            RecordPrefab.tag = "Record";
            _animOccurred = true;
            rightSide = arg.interactorObject.transform.CompareTag("LeftHand");
            StartCoroutine("RecordSlide");
        }
    }

    //Unparent record and add Grabbable Component
    public void DetachRecord()
    {
        RecordPrefab.transform.parent = null;
        Debug.Log("RecordDetached");

        RecordPrefab.AddComponent<BoxCollider>();
        Rigidbody rb = RecordPrefab.AddComponent<Rigidbody>();
        RecordPrefab.AddComponent<XRGrabInteractable>();

        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        InteractionLayerMask record = LayerMask.NameToLayer("RecordSocket");
        XRGrabInteractable recordGrabbable = RecordPrefab.GetComponent<XRGrabInteractable>();
        recordGrabbable.useDynamicAttach = true;
        recordGrabbable.interactionLayers = record;
        RecordPrefab.tag = "RecordPrefab";
    }

    //animate record sliding out of case with coroutine
    private IEnumerator RecordSlide()
    {
        Vector3 initial = gameObject.transform.position;
        Vector3 target = (rightSide ? gameObject.transform.GetChild(0).position : gameObject.transform.GetChild(1).position);
        Vector3 current = initial;
        float timer = 0;

        Debug.Log("CalledCoroutine");
        while (Vector3.Distance(current, target) > 0.05)
        { 
            Debug.Log("Inside While Loop");
            current = Vector3.Lerp(initial, target, timer / slideTime);
            timer += Time.deltaTime;
            RecordPrefab.transform.position = current;

            //update position to lerp from/to
            target = (rightSide ? gameObject.transform.GetChild(0).position : gameObject.transform.GetChild(1).position);
            initial = gameObject.transform.position;

            yield return null;
        }
        Debug.Log("animFinished");
        DetachRecord();
    }
}
