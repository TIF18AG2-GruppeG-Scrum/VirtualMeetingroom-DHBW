using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using SimpleFileBrowser;

public class FileManager : MonoBehaviour
{
    string path;
    public Sprite PDFIcon;
    public RawImage image;
    public VideoPlayer videoPlayer;
    private bool IsClosed = true;

    // Start is called before the first frame update
    void Start()
    {
        FileBrowser.SetExcludedExtensions(".lnk",".tmp",".zip",".rar",".exe");
        FileBrowser.AddQuickLink("Users", "c:\\Users");
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsClosed)
        {
            Cursor.lockState = CursorLockMode.None;
            IsClosed = false;
            StartCoroutine(ShowDialogCoroutine());
        }
    }
    public IEnumerator ShowDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(false, @"C:\", "Load File","Load");
        Debug.Log(FileBrowser.Success +" "+FileBrowser.Result);
        IsClosed = false;
        if (FileBrowser.Success)
        {
            videoPlayer.url = FileBrowser.Result;
            videoPlayer.Play();
        }
    }
}
