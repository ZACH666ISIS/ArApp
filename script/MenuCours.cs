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
    public TextMeshProUGUI wlc,age;
    DateTime dateCurrent = DateTime.Now;
    int x;
    void Awake()
    {
        
            string readFile = @"Assets\\Resources\\storage\\info.txt";
            string[] line = System.IO.File.ReadAllLines(readFile);
            string[] info = line[0].Split("|");
            wlc.text += info[0];
            x =dateCurrent.Year-Int32.Parse(info[1]);
            age.text += x;





    }
    public void setCours(string cours)
    {
        ParVar.cours = cours;
        SceneManager.LoadScene(1);
    }




}