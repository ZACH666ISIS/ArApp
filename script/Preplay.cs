using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class Preplay : MonoBehaviour
{
    public GameObject popUp;
    public TextMeshProUGUI inputUser,inputYears,popInfo;
    public Button btnExit, btnSend;
    private string user,years,pathStrg= "Assets\\Resources\\storage\\info.txt";
    private int date;
    void Awake()
    {
        if (System.IO.File.Exists(pathStrg))SceneManager.LoadScene(1);
    }
    void Start()
    {
        popUp.SetActive(false);
        btnSend.onClick.AddListener(RetInfo);
        btnExit.onClick.AddListener(OnPop);

    }

 
    void OnPop()
    {
        popUp.SetActive(false);

    }
    void RetInfo()
    {
        user = inputUser.text;
        years = inputYears.text;
        Regex rgx = new Regex(@"\d{4}");
        if (user.Length < 13)
        {
            if (rgx.IsMatch(years) && years.Length==5 )
            {
                System.IO.FileStream oFileStream = null;
                oFileStream = new System.IO.FileStream(pathStrg, System.IO.FileMode.Create);
                oFileStream.Close();
                StreamWriter writer = new StreamWriter(pathStrg, true);
                writer.WriteLine(user+"|"+years);
                writer.Close();
            }

            else
            {
                popInfo.text = "Verify your birth years";
                popUp.SetActive(true);
            }


        }


        else
        {
            popInfo.text = "Length of username should be under 12 characters";
            popUp.SetActive(true);
        }
      
        

    }
   
}
