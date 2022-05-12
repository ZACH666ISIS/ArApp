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


public class MenuLang : MonoBehaviour
{
    public GameObject loading;
    public TextMeshProUGUI txtWlc;
    private Data matLang;
    void Awake()
    {
        matLang = SaveSystem.LoadInfo();
        
        string line = Resources.Load<TextAsset>("storage/selectLang").text;
        string[] param = line.Split('|');
        line = null;
        txtWlc.text = param[matLang.GetMatLang()];
        param = null;
        if (matLang.GetMatLang() == 2) txtWlc.fontSize = 50;
        loading.SetActive(false);
    }
        public void setLang(string lang)
    {
        ParVar.lang = lang;
        StartCoroutine(LoadYourAsyncScene(3));

    }
     public void retour()
        {
            StartCoroutine(LoadYourAsyncScene(1));
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

   



}