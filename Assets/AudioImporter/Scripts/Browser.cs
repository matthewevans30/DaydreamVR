using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// A file browser.
/// </summary>
public class Browser : MonoBehaviour
{
    /// <summary>
    /// Occurs when a file has been selected in the browser.
    /// </summary>
    public event Action<string> FileSelected;
    
    /// <summary>
    /// File extensions that will show up in the browser.
    /// </summary>
    public List<string> extensions;

    public GameObject listItemPrefab;

    public GameObject upButton;
    public ScrollRect scrollRect;
    public GameObject folderPanel;
    public GameObject filePanel;

    private string currentDirectory;
    private string[] drives;
    private List<string> directories;
    private List<string> files;
    private bool selectDrive;
    private bool scrolling;

    public static List<string> selectedWavPaths;
    public TextMeshProUGUI songDisplay;

    public static List<string> selectedCoverPaths;
    public TextMeshProUGUI coverDisplay;

    public static bool useCustomFiles;

    void Awake()
    {
        extensions = new List<string>();

        string WAVExtension = ".wav";
        string PNGExtension = ".png";
        string JPGExtension = ".jpg";
        string JFIFExtension = ".jfif";

        extensions.Add(WAVExtension);
        extensions.Add(PNGExtension);
        extensions.Add(JPGExtension);
        extensions.Add(JFIFExtension);

        directories = new List<string>();
        files = new List<string>();
        selectedWavPaths = new List<string>();
        selectedCoverPaths = new List<string>();

        drives = Directory.GetLogicalDrives();
        currentDirectory = PlayerPrefs.GetString("currentDirectory", "");

        selectDrive = (string.IsNullOrEmpty(currentDirectory) || !Directory.Exists(currentDirectory));

        BuildContent();
    }

    /// <summary>
    /// Go to the current directory's parent directory.
    /// </summary>
    public void Up()
    {
        if (currentDirectory == Path.GetPathRoot(currentDirectory))
        {            
            selectDrive = true;
            ClearContent();
            BuildContent();
        }
        else
        {
            currentDirectory = Directory.GetParent(currentDirectory).FullName;
            
            ClearContent();
            BuildContent();
        }
    }
   
    private void BuildContent()
    {       
        directories.Clear();
        files.Clear();
        
        if (selectDrive)
        {
            directories.AddRange(drives);
            StopAllCoroutines();
            StartCoroutine(refreshDirectories());
            return;
        }
        
        try
        {
            directories.AddRange(Directory.GetDirectories(currentDirectory));
            
            foreach (string file in Directory.GetFiles(currentDirectory))
            {
                if (extensions.Contains(Path.GetExtension(file)))
                    files.Add(file);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

        StopAllCoroutines();
        StartCoroutine(refreshFiles());
        StartCoroutine(refreshDirectories());

        //if (directories.Count + files.Count == 0)
            EventSystem.current.SetSelectedGameObject(upButton);
    }

    private void ClearContent()
    {
        Button[] children = filePanel.GetComponentsInChildren<Button>();

        foreach (Button child in children)
            Destroy(child.gameObject);

        children = folderPanel.GetComponentsInChildren<Button>();

        foreach (Button child in children)
            Destroy(child.gameObject);
    }

    private void OnFileSelected(int index)
    {
        string path = files[index];
        string extension = Path.GetExtension(path);
        if (extension.Equals(".wav"))
        {
            AddSongFile(path);
        }
        else if (extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jfif"))
        {
            AddCoverArtFile(path);
        }
    }

    void AddSongFile(string path)
    {
        if (selectedWavPaths.Contains(path))
        {
            selectedWavPaths.Remove(path);
        }
        else if (selectedWavPaths.Count > 6)
        {
            return;
        }
        else
        {
            selectedWavPaths.Add(path);
        }

        UpdateSongText();
    }

    void AddCoverArtFile(string path)
    {
        if (selectedCoverPaths.Contains(path))
        {
            selectedCoverPaths.Remove(path);
        }
        else if (selectedCoverPaths.Count > 6)
        {
            return;
        }
        else
        {
            selectedCoverPaths.Add(path);
        }

        UpdateCoverArtText();
    }

    void UpdateSongText()
    {
        int index = 1;
        string displayText = "Song Files: \n";
        foreach (string filePath in selectedWavPaths) {
            displayText += index + ". ";
            displayText += filePath.Substring(filePath.LastIndexOf("\\") + 1);
            displayText += "\n";
            index++;
        }
        songDisplay.text = displayText;
    }

    void UpdateCoverArtText()
    {
        int index = 1;
        string displayText = "Cover Art: \n";
        foreach (string filePath in selectedCoverPaths)
        {
            displayText += index + ". ";
            displayText += filePath.Substring(filePath.LastIndexOf("\\") + 1);
            displayText += "\n";
            index++;
        }
        coverDisplay.text = displayText;
    }

    private void OnDirectorySelected(int index)
    {
        if (selectDrive)
        {
            currentDirectory = drives[index];
            selectDrive = false;
        }
        else
        {
            currentDirectory =  directories[index];
        }

        ClearContent();
        BuildContent();
    }

    IEnumerator refreshFiles()
    {
        for (int i = 0; i < files.Count; i++)
        {
            AddFileItem(i);          
                        
            yield return null;
        }
    }

    IEnumerator refreshDirectories()
    {
        for (int i = 0; i < directories.Count; i++)
        {
            AddDirectoryItem(i);
            
            yield return null;
        }
    }

    private void AddFileItem(int index)
    {
        GameObject listItem = Instantiate(listItemPrefab);

        Button button = listItem.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            OnFileSelected(index);
        });

        listItem.GetComponentInChildren<Text>().text = Path.GetFileName(files[index]);
        listItem.transform.SetParent(filePanel.transform, false);
    }

    private void AddDirectoryItem(int index)
    {
        GameObject listItem = Instantiate(listItemPrefab);

        Button button = listItem.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            OnDirectorySelected(index);
        });

        if (selectDrive)
            listItem.GetComponentInChildren<Text>().text = directories[index];
        else
            listItem.GetComponentInChildren<Text>().text = Path.GetFileName(directories[index]);

        listItem.transform.SetParent(folderPanel.transform, false);
    }
       
    void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;

        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        if (selected != null)
        {
            if (selected.transform.IsChildOf(transform))
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Escape))
                    Up();

                if (Mathf.Abs(Input.GetAxis("Vertical")) > .3f)
                {
                    if (selected.transform.IsChildOf(transform))
                    {
                        scrollRect.movementType = ScrollRect.MovementType.Clamped;
                        RectTransform rt = selected.GetComponent<RectTransform>();
                        Vector2 dif = scrollRect.transform.position - rt.position;

                        if (Mathf.Abs(dif.y) > .5f)
                        {
                            Vector2 scrollVelocity = Vector2.zero;
                            scrollVelocity.y = dif.y * 3;
                            scrollRect.velocity = scrollVelocity;
                        }

                        scrolling = true;
                    }
                }
                else if (scrolling)
                {
                    if (scrollRect.verticalNormalizedPosition > .99f || scrollRect.verticalNormalizedPosition < .01f)
                        scrollRect.StopMovement();
                    scrolling = false;
                }
            }
        }             
    }
}