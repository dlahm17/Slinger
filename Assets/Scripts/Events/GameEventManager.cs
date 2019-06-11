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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
