using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Data 
{


    private int matLang;

    public Data(int matLang)
    {
        this.matLang=matLang;
    }

    public int GetMatLang()
    {
        return matLang;
    }
   
}
