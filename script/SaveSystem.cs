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
        Data d = new Data(matLang);
        formatter.Serialize(fs, d);
        fs.Close();
    }

    public static Data LoadInfo()
    {
        string path = Application.persistentDataPath + "/info.iori";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            Data n = formatter.Deserialize(fs) as Data;
            fs.Close();
            return n;
        }
        else
        {
            return null;
        }

    }

}
