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
    public int savefile = 1;
    float percentageSaved;
    // Start is called before the first frame update
    void Start()
    {
        updateSaveFile();
        
    }
    public void updateSaveFile()
    {

        string dataP = "GameData_" + savefile.ToString() + ".hzg";
        dataPath = Path.Combine(Application.persistentDataPath, dataP);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GDat.saveFile = savefile;
            saveTime(GDat);
            Debug.Log("Getting Items");
            int x = 0;
            while (x < 20)
            {
                if(Inventory.instance.getItem(x) == null)
                {
                    Debug.Log("Fucky");
                    break;
                }
                GDat.inventoryItems[x].It = Inventory.instance.getItem(x);
                Debug.Log("Item " + x + " : " + GDat.inventoryItems[x].It);
                x++;
            }
            x = 0;
            Debug.Log("Getting Equipment");
            while (x < 5)
            {
                if (EquipmentManager.instance.getEquipment(x) == null)
                {
                    break;
                }
                GDat.Player_Equipment[x].Eq = EquipmentManager.instance.getEquipment(x);
                 Debug.Log("Equipment " + x + " : " + GDat.Player_Equipment[x].Eq);
                 x++;
            }
            saveMyData(GDat, dataPath);
        }

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
    //The time, items, and equipment are all saved when the game gets saved.
    //All other save data bits must be saved from that item itself.
    public void saveTime(GameData data)
    {
        Debug.Log("Getting Time");
        data.time = Time.time;
    }
    /*
    public void saveItems(GameData data)
    {
        Debug.Log("Getting Items");
        int x = 0;
        while(x < 20)
        {
            Debug.Log(Inventory.instance.items[x]);
            if (Inventory.instance.items[x] != null)
            {
                data.inventoryItems[x].It = Inventory.instance.items[x];
                Debug.Log("Item " + x + " : " + data.inventoryItems[x].It);
            }
            x++;
        }
    }
    public void saveEquipment(GameData data)
    {
        Debug.Log("Getting Equipment");
        int x = 0;
        while(x < 5)
        {
            if (EquipmentManager.instance.getEquipment(x) != null)
            {
                data.Player_Equipment[x].Eq = EquipmentManager.instance.getEquipment(x);
                Debug.Log("Equipment " + x + " : " + data.Player_Equipment[x].Eq);
            }
            x++;
        }
    }
    */
    




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
