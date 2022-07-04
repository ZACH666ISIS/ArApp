using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParVar 
{
    //class aide a cité des données persistantes en temps d'execution 
    public static string lang;
    public static string cours;
    public static string pathObjet;
    public static string pathSound;
    public static int nbrObj;
    public static bool tr=false;
    public static void SetPath()
    {
        if (cours == "alphabet")
            pathObjet = "obj/"+cours + "/" + lang + "/";
        else
            pathObjet = "obj/" + cours + "/";
        pathSound = "ArSound/" + cours + "/" + lang + "/";
        var audioClip = Resources.LoadAll<AudioClip>(pathSound);
        nbrObj = audioClip.Length;
        audioClip = null;
    }
    
}
