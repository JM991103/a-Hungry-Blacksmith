using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static string SavePath => Application.persistentDataPath + "/saves/";
        
    public static void Save(SaveData saveData, string saveFileName)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }
    }
}
