using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SetupSaves : MonoBehaviour
{
    string basePath;
    string curPath;
    // Start is called before the first frame update
    void Start()
    {
        basePath = Application.persistentDataPath;
        Debug.Log(basePath);
        for (int i = 1; i < 11; i++)
        {
            string assistPath = "GameData_" + i + ".hzg";
            curPath = Path.Combine(basePath, assistPath);
            if (!File.Exists(curPath))
            {
                Debug.Log("File " + i + " does not exist");
                GameData GDat = new GameData();
                string jsonString = JsonUtility.ToJson(GDat);

                using (StreamWriter streamWriter = File.CreateText(curPath))
                {
                    streamWriter.Write(jsonString);

                }
            }
        }
    }
    
}
