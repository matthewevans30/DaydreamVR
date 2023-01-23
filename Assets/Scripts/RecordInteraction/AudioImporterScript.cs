using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioImporterScript : MonoBehaviour
{
    public Browser browser;
    public AudioImporter importer;
    public AudioSource audioSource;

    void Awake()
    {
        browser.FileSelected += OnFileSelected;
        importer.Loaded += OnLoaded;
    }
    private void OnFileSelected(string path)
    {
        importer.Import(path);
    }
    private void OnLoaded(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}