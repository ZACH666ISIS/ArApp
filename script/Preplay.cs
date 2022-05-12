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

    public GameObject loading;

  
    void Awake()
    {
    if (SaveSystem.LoadInfo()!=null)SceneManager.LoadScene(1);
    }
    void Start()
    {
        loading.SetActive(false);
       

    }

 


    IEnumerator LoadYourAsyncScene(int x)
    {


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(x);

        loading.SetActive(true);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        loading.SetActive(false);
    }
    public void RetInfo(int y)
    {
        
                SaveSystem.SetInfo(y);
                StartCoroutine(LoadYourAsyncScene(1));

    }
   
}
