using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CapturePhoto : MonoBehaviour
{
    // Source texture and the rectangular area we want to extract.
    public RenderTexture sourceTex;
    public GameObject polaroid;
    public AudioSource audio;

    float printTime = 2f;

    GameObject newPolaroid;

    public void TakePhoto()
    {
        audio.Play();
        newPolaroid = Instantiate(polaroid, gameObject.transform.GetChild(0));
        StartCoroutine(CameraFlash());
        StartCoroutine(SetPhotoTexture());
        StartCoroutine(PrintPhoto());
    }

    public IEnumerator SetPhotoTexture()
    {
        yield return new WaitForEndOfFrame();
        //camera.rect = new Rect(0, 0, 1, 1);
        RenderTexture og = RenderTexture.active;
        Texture2D photoTex = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.RGBA32, false);
        RenderTexture.active = sourceTex;
        photoTex.ReadPixels(new Rect(0, 0, sourceTex.width, sourceTex.height), 0, 0);
        photoTex.Apply();
        //camera.rect = new Rect(0, 0, 0, 0);
        RenderTexture.active = og;

        newPolaroid.transform.GetChild(1).GetComponent<Renderer>().material.SetTexture("_BaseMap", photoTex);


    }

    IEnumerator CameraFlash() {
        transform.GetChild(5).GetComponent<Light>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        transform.GetChild(5).GetComponent<Light>().enabled = false;

    }

    IEnumerator PrintPhoto()
    {
        Vector3 initial = gameObject.transform.GetChild(0).position;
        Vector3 target = gameObject.transform.GetChild(1).position;
        Vector3 current = initial;
        float timer = 0;

        //Debug.Log("CalledCoroutine");
        while (Vector3.Distance(current, target) > 0.05)
        {
            //Debug.Log("Inside While Loop");
            current = Vector3.Lerp(initial, target, timer / printTime);
            timer += Time.deltaTime;
            newPolaroid.transform.position = current;

            //update position to lerp from/to
            target = gameObject.transform.GetChild(1).position;
            initial = gameObject.transform.GetChild(0).position;

            yield return null;
        }
        Debug.Log("animFinished");
        //yield return new WaitForSeconds(1);
        //DetachPicture();
    }

    void DetachPicture()
    {
        newPolaroid.transform.parent = null;
        newPolaroid.GetComponent<Rigidbody>().useGravity = true;
    }
}
