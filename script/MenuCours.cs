using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuCours : MonoBehaviour
{
    public TextMeshProUGUI[] coursName = new TextMeshProUGUI[8];
    public GameObject loading,listLang;
    public Button btnSetting;
    private User matLang;
    void Awake()
    {
        
        listLang.SetActive(false);
        loading.SetActive(false);
        matLang = SaveSystem.LoadInfo();
        string temp = Resources.Load<TextAsset>("storage/lang").text;
        string[] lines = temp.Split("\n");
        temp = null;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] param = lines[i].Split('|');
            coursName[i].text = param[matLang.GetMatLang()];
        }
        lines = null;
        

        btnSetting.onClick.AddListener(() => Aff());

    }
    public void Aff()
    {
        if(listLang.activeSelf)
            listLang.SetActive(false);
        else listLang.SetActive(true);
    }

    public void setCours(string cours)
    {
        ParVar.cours = cours;
        StartCoroutine(LoadYourAsyncScene(2));

    }
    //function asynchron avant le chargement du scene
    IEnumerator LoadYourAsyncScene(int s)
            {
                

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(s);

        loading.SetActive(true);
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
        loading.SetActive(false);
    }

    public void ChangeLang(int h)
    {
        SaveSystem.SetInfo(h);
        StartCoroutine(LoadYourAsyncScene(1));
    }
    public void sort()
    {
        Application.Quit();
    }

}