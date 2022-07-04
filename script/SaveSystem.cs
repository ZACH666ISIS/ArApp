using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    public static void SetInfo(int matLang)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/info.iori";
        FileStream fs = new FileStream(path, FileMode.Create);
        User d = new User(matLang);
        formatter.Serialize(fs, d);
        fs.Close();
    }

    public static User LoadInfo()
    {
        string path = Application.persistentDataPath + "/info.iori";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            User n = formatter.Deserialize(fs) as User;
            fs.Close();
            return n;
        }
        else
        {
            return null;
        }

    }

}
