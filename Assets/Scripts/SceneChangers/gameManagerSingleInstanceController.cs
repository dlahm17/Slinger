using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerSingleInstanceController : MonoBehaviour
{
    //This prevents there being multiple instances of the player/game manager in the game at the same time.
   
    void Start()
    {
        List<gameManagerSingleInstanceController> gmanage = new List<gameManagerSingleInstanceController>();
        gameManagerSingleInstanceController[] gameManagers;
        gameManagers = GameObject.FindObjectsOfType<gameManagerSingleInstanceController>();
        foreach(gameManagerSingleInstanceController gameManager in gameManagers)
        {
            gmanage.Add(gameManager);
        }
        if(gmanage.Count > 1)
        {
            Debug.Log("Too many of us, destroying myself");
            Destroy(gameObject);
        }
    }
}
