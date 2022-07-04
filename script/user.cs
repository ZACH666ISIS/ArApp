using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class User
{


    private int matLang;
    private bool ent;

    public User(int matLang)
    {
        this.matLang=matLang;
    }

    public int GetMatLang()
    {
        return matLang;
    }
   
}
