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
        GDat.saveFile = savefile;
        dataPath = Path.Combine(Application.persistentDataPath, dataP);
        //Debug.Log(dataPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Saving Data");
            GDat.saveFile = savefile;
            saveTime(GDat);
            GDat.exp = (int)Inventory.instance.checkExp();
            GDat.gold = (int)Inventory.instance.checkGold();
            Debug.Log("Getting Items");
            int x = 0;
            while (x < 20)
            {
                if(Inventory.instance.getItem(x) == null)
                {
                    break;
                }
                GDat.inventoryItems[x].It = Inventory.instance.getItem(x);
                Debug.Log("Item " + x + " : " + GDat.inventoryItems[x].It);
                x++;
            }
            x = 0;
            while (x < 5)
            {
                if (EquipmentManager.instance.getEquipment(x) == null)
                {
                    break;
                }
                GDat.Player_Equipment[x].Eq = EquipmentManager.instance.getEquipment(x);
                 x++;
            }
            saveMyData(GDat, dataPath);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Loading data");
            GDat = loadMyData(dataPath);
            applyData();
        }
    }

    public void applyData()
    {
        playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
        pst.resetAllStats();

        EquipmentManager eqM = EquipmentManager.instance;
        for(int x = 0; x < 5; x++)
        {
            eqM.Equip(GDat.Player_Equipment[x].Eq);
            
        }
        Inventory In = Inventory.instance;
        In.RemoveAll();
        for (int x = 0; x < 20; x++)
        {
            if(GDat.inventoryItems[x].It == null)
            {
                break;
            }
            In.Add(GDat.inventoryItems[x].It);
            
        }

        GameObject[] n = GameObject.FindGameObjectsWithTag("Node");
        foreach(GameObject ni in n)
        {
            ni.GetComponent<node>().loadData();
        }

        Inventory.instance.resetGold();
        Inventory.instance.giveGold(GDat.gold);
        Inventory.instance.resetExp();
        Inventory.instance.giveExp(GDat.exp);

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
        data.time += Time.time;
    }
    
    




    static GameData loadMyData(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<GameData>(jsonString);
        }
    }
}
