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
 
    public void setLang(string lang)
    {
        ParVar.lang = lang;
        SceneManager.LoadScene(2);
    }
    public void retour()
    {
        SceneManager.LoadScene(0);
    }



}