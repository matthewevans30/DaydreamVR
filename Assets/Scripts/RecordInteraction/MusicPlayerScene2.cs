using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MusicPlayerScene2 : MonoBehaviour
{
    AudioSource record;
    bool _hasRecord;
    bool _isSpinning;
    bool _armDown;
    bool _recordPaused;
    bool _stillPlaying;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "RecordArm")
        {
            _armDown = true;
            Debug.Log("ArmDown");

            if (_hasRecord && _isSpinning)
            {
                if (_recordPaused)
                {
                    record.UnPause();
                }
                else if (_stillPlaying)
                {
                    record.volume = 1;
                }
                else
                {
                    record.Play();
                    record.volume = 1;
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "RecordArm")
        {
            _armDown = false;
            Debug.Log("ArmUp");
            if (_hasRecord && _isSpinning)
            {
                record.volume = 0;
                _stillPlaying = true;
            }
        }
    }

    public void OnSelected(SelectEnterEventArgs arg)
    {
        if (arg.interactableObject.transform.tag == "RecordPrefab")
        {
            record = arg.interactableObject.transform.GetComponent<AudioSource>();
            AudioAnalysis.audiosource = record;
            _hasRecord = true;
            Debug.Log("Record Placed");
        }
    }

    public void OnSelectExit(SelectExitEventArgs arg)
    {
        if (arg.interactableObject.transform.tag == "RecordPrefab")
        {
            _hasRecord = false;
            _recordPaused = false;
            _stillPlaying = false;
            _isSpinning = false;
            record.Stop();
            Debug.Log("Record Removed");
            AudioAnalysis.audiosource = null;
        }
    }

    public void startSpinning()
    {
        _isSpinning = true;
        Debug.Log("IsSpinning");
        if (_hasRecord && _armDown)
        {
            Debug.Log("RecordPlaying");
            if (_recordPaused)
            {
                Debug.Log("UnpausingRecord");
                record.UnPause();
            }
            else
            {
                record.Play();
                record.volume = 1;
            }
            //animate record spin
        }
        else if (_armDown && !_hasRecord)
        {
            //play low static
            //animate plate spin
        }

    }

    public void stopSpinning()
    {
        _stillPlaying = false;
        Debug.Log("StopSpinning");
        if (_isSpinning)
        {
            Debug.Log("IsSpinning = false");
            _isSpinning = false;
            if (record != null && record.isPlaying)
            {
                record.Pause();
                _recordPaused = true;
                //stop record spin
            }
            else if (_armDown && !_hasRecord)
            {
                //pause low static
                //stop animate plate spin
            }
        }
    }


}