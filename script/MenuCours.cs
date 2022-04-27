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


public class MenuCours : MonoBehaviour
{
 
    public void setCours(string cours)
    {
        ParVar.cours = cours;
        SceneManager.LoadScene(1);
    }




}