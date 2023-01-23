using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class CustomRecordLoader : MonoBehaviour
{
    public List<GameObject> records = new List<GameObject>();

    async void Start()
    {
        if (Browser.useCustomFiles)
        {
            int index = 0;
            foreach (string songpath in Browser.selectedWavPaths)
            {
                //build absolute path
                var path = Browser.selectedWavPaths[index];

                // wait for the load and set your property
                records[index].transform.GetChild(2).GetComponent<AudioSource>().clip = await LoadClip(path);
                index++;
            }

            index = 0;

            foreach(string coverPath in Browser.selectedCoverPaths)
            {
                var path = Browser.selectedCoverPaths[index];

                records[index].transform.GetChild(3).transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", LoadTEX(path));
                index++;
            }
        }

            //... do something with it
        }

    async Task<AudioClip> LoadClip(string path)
    {
        AudioClip clip = null;
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            uwr.SendWebRequest();

            // wrap tasks in try/catch, otherwise it'll fail silently
            try
            {
                while (!uwr.isDone) await Task.Delay(5);

                if (uwr.isNetworkError || uwr.isHttpError) Debug.Log($"{uwr.error}");
                else
                {
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
            catch (Exception err)
            {
                Debug.Log($"{err.Message}, {err.StackTrace}");
            }
        }
        return clip;
    }

    public static Texture2D LoadTEX(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

}
