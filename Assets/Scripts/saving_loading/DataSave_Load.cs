using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSave_Load : MonoBehaviour
{
    #region singleton
    public static DataSave_Load instance;
    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("more than one save and load system in scene");
            return;
        }
        instance = this;
    }

    #endregion
    string dataPath;
    public GameData GDat;
    // Start is called before the first frame update
    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "GameData.txt");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            saveMyData(GDat, dataPath);

        if (Input.GetKeyDown(KeyCode.L))
            GDat = loadMyData(dataPath);
    }

    static void saveMyData(GameData data, string path)
    {
        Debug.Log("saving");
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }
    static GameData loadMyData(string path)
    {
        Debug.Log("Loading");
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<GameData>(jsonString);
        }
    }
}
