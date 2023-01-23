using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordArmFix : MonoBehaviour
{
    GameObject interactor;
    bool _selected;

    public void onSelectEntered(SelectEnterEventArgs arg)
    {
        interactor = arg.interactorObject.transform.gameObject;
        _selected = true;
        //transform.Rotate(0, 180, 0);
        StartCoroutine("FixRotation");


    }

    public void onSelectExit()
    {
        _selected = false;
        StopCoroutine("FixRotation");
    }

    private IEnumerator FixRotation()
    {
        while (_selected)
        {
            //Debug.Log("transforming");
            float angle = interactor.transform.rotation.eulerAngles.y;
            Debug.Log(angle);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return null;
        }
        
    }
}
