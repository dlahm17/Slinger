using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    #region singleton
    public static GameEventManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than once event manager in scene, deleting");
            Destroy(this);
            return;
        }
        instance = this;
    }
    #endregion
    DataSave_Load Dat;

    private void Start()
    {
        if (DataSave_Load.instance != null)
        {
            Dat = DataSave_Load.instance;
        }
        
    }

    public bool queryEventCompletion(int ID)
    {
        if (Dat != null)
        {
            if (Dat.GDat.eventData[ID].hasHappened)
            {
                return true;
            }
        }
            return false;
        
    }

    public void SetEvent(int ID, bool hasHapp)
    {
        if (Dat != null)
        {
            Dat.GDat.eventData[ID].hasHappened = hasHapp;
            Dat.Save();
        }
    }
}
