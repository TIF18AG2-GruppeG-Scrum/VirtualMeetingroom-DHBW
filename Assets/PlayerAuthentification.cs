using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class PlayerAuthentification : MonoBehaviour
{
    public static bool IsAdmin;
    public TMP_InputField adminName;
    public TMP_InputField adminPassword;
    public TMP_InputField guestName;
    public TMP_InputField ipAdress;
    public TMP_InputField oldPass;
    public TMP_InputField newPass;
    public static string Name;
    public static string Password;
    public static string IpAdress;
    public Text nameLabel;
    


    private void SetIsAdmin()
    {
        IsAdmin = true;
    }

    private void SetIsGuest()
    {
        IsAdmin = false;
    }

    public void saveIP()
    {
        if (!string.IsNullOrWhiteSpace(ipAdress.text))
            IpAdress = ipAdress.text;
    }

    public void CheckAdminInput()
    {
    
       
        if (!string.IsNullOrWhiteSpace(adminName.text) && !string.IsNullOrWhiteSpace(adminPassword.text))
        {
          
            Name = adminName.text;
            if (nameLabel != null)
            nameLabel.text = Name;
            Password = adminPassword.text;

            if (readAdminTextFile().Equals(Password))
            {
                SetIsAdmin();
                LoadRoom();
                
            }
            else
            {
                adminName.text = "";
                adminPassword.text = "Password is not correct";
            }
        }
    }

    void Start(){
        
        if (nameLabel != null)
        nameLabel.text = Name;

    }

  

    public void EnterGuest()
    {
        bool hadLoggedIn = Name != null;

        if (!string.IsNullOrWhiteSpace(guestName.text))
        {
            SetIsGuest();
            Name = guestName.text;
            guestName.text = "";
            if (nameLabel != null)
            nameLabel.text = Name;
            if(!hadLoggedIn)
             LoadRoom();
           
        }
    }

    public void SetNewPass()
    {
        if (readAdminTextFile().Equals(oldPass.text))
        {
            File.WriteAllText(Application.dataPath + @"/Admin.txt", newPass.text);
        }
        else
        {
            oldPass.text = "oldpassword is not correct";
        }
    }

    private void LoadRoom()
    {
        SceneManager.LoadScene(0);
    }

    private string readAdminTextFile()
    {
        return File.ReadAllText(Application.dataPath + @"/Admin.txt");
    }
}
