using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonVR : MonoBehaviour
{
    [SerializeField] public float threshold = 0.45f;        //inverse percentage of button pressed needed to activate button
    [SerializeField] public float deadZone = 0.025f;

    private bool _isPressed;                //state management
    public Vector3 _startPosition;         //compare to current pos to tell how far button is moved  

    public UnityEvent onPressed, onReleased;
    GameObject presser;

    public ConfigurableJoint _joint;
    public bool active;


    // Start is called before the first frame update
    public virtual void Start()
    {
        _isPressed = false;
        _joint = GetComponent<ConfigurableJoint>();
        _startPosition = transform.localPosition;
        //Debug.Log(_startPosition);

    }

    public virtual void FixedUpdate()
    {
        if (active)
        {
            if (!_isPressed && GetValue() + threshold >= 1)
            {
                //Debug.Log("Pressed Val : " + (GetValue() + threshold));
                Pressed();
            }
            if (_isPressed && GetValue() - threshold <= 0)
            {
                Released();

            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("RightHand") || other.CompareTag("LeftHand")))
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.SetActive(true);
            active = true; 
        }

    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightHand") || other.CompareTag("LeftHand"))
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.SetActive(false);
            //StartCoroutine(DeactivateUpdate());
        }
    }

    public virtual float GetValue()
    {
        var value = Vector3.Distance(_startPosition, transform.localPosition) / _joint.linearLimit.limit;
        //Debug.Log(transform.localPosition);

        if(Mathf.Abs(value) < deadZone)
        {
            return 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    public void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("pressed");
    }

    public void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("released");
        active = false;
    }

    //sets active to false after 4 seconds so as not to be doing calculations
    //every fixed update frame
    public IEnumerator DeactivateUpdate()
    {
        yield return new WaitForSeconds(4);
        active = false;
    }


    
}
