using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using SimpleFileBrowser;
using System.IO;
using System;
using System.Linq;
public class FileManager : MonoBehaviour
{
   
    public Sprite PDFIcon;
    public VideoPlayer videoPlayer;
    private bool IsClosed = true;
    public string DestFolder;
    private string VideoDocpath;
    private string DirectoryPath;
    public RawImage RawImage;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        FileBrowser.SetExcludedExtensions(".lnk",".tmp",".zip",".rar",".exe");
        FileBrowser.AddQuickLink("Users", "c:\\Users");
        DirectoryPath = Application.dataPath + "/Resources/Boards/"+DestFolder;

        if (PDFIcon != null)
        {
            FileBrowser.AddExtension(".pdf", PDFIcon);
        }
      
        if(player != null && !player.GetComponent<PlayerAuthentification>().IsAdmin)
        {
            gameObject.SetActive(false);

        }

    }

    // Update is called once per frame
    public void UploadVideoCoroutine()
    {
        if (IsClosed)
        {
            IsClosed = false;
            StartCoroutine(UploadVideo());
        }

    }

    private  IEnumerator UploadVideo()
    {
        yield return FileBrowser.WaitForLoadDialog(false, @"C:\", "Load File","Load");
        Debug.Log(FileBrowser.Success +" "+FileBrowser.Result);
        IsClosed = true;

        if (FileBrowser.Success)
        {
            try
            {
              
                videoPlayer.url = FileBrowser.Result;

             
                if(videoPlayer.url != FileBrowser.Result)
                    throw new Exception("Falsche Format oder wird nicht unterstützt");
             
                videoPlayer.Stop();
                ClearFolder();
                File.Copy(FileBrowser.Result, DirectoryPath + "/" + FileBrowser.Result.Substring(FileBrowser.Result.LastIndexOf('\\') + 1));
                VideoDocpath = DirectoryPath + "/" + FileBrowser.Result.Substring(FileBrowser.Result.LastIndexOf('\\') + 1);
            }

            catch (Exception e)
            {
              throw new Exception(e.Message); 
            }
            finally
            {
              
            }

        }
    }

    private void ClearFolder()
    {
        string[] filePaths = Directory.GetFiles(DirectoryPath + "/");
        foreach (string filePath in filePaths)
            File.Delete(filePath);

    }
    public void OpenDocument()
    {
        try
        {
            
            VideoDocpath = Directory.GetFiles(DirectoryPath + "/").FirstOrDefault();

            if (VideoDocpath != null)
            {
               
                Application.OpenURL(VideoDocpath);
            }

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {

        }
        
    }

    public void UploadDocumentCoroutine()
    {
        if (IsClosed)
        {
            IsClosed = false;
            StartCoroutine(UploadDocument());
        }

    }

    private IEnumerator UploadDocument()
    {
        yield return FileBrowser.WaitForLoadDialog(false, @"C:\", "Load File", "Load");
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
        IsClosed = true;

        if (FileBrowser.Success)
        {
            try
            {
                
                ClearFolder();
                File.Copy(FileBrowser.Result, DirectoryPath + "/" + FileBrowser.Result.Substring(FileBrowser.Result.LastIndexOf('\\') + 1));
                VideoDocpath = DirectoryPath + "/" + FileBrowser.Result.Substring(FileBrowser.Result.LastIndexOf('\\') + 1);

            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {

            }

        }
    }

    public void PlayVideo()
    {
       
        try
        {
            if(VideoDocpath == null)
             VideoDocpath = Directory.GetFiles(DirectoryPath + "/").FirstOrDefault();

            if (VideoDocpath != null)
            {
                videoPlayer.url = VideoDocpath;

                if(videoPlayer.url != VideoDocpath)
                {
                    throw new Exception("Falsche Videoformat oder es wird derzeit nicht unterstützt");
                }

                videoPlayer.Play();
            }
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {

        }
    }

    public void StopVideo()
    {
        try
        {
            videoPlayer.Stop();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {

        }
    }

}
