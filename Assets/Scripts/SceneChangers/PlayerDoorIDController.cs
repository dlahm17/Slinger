using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDoorIDController : MonoBehaviour
{
    /// <summary>
    /// the ID controller controls which door you start off at
    /// </summary>
    #region singleton
    public static PlayerDoorIDController instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    Door ConnectedDoor;
    int IDToFind;

    GameObject targetGO;
    GameObject[] doors;

    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += onLevelWasLoaded;
    }
    public void changeTheScene(Door interactedDoorID)
    {
        string sceneToChangeTo = "Debug";
        sceneToChangeTo = interactedDoorID.sceneToChangeTo;
        
        IDToFind = interactedDoorID.myID;
        Debug.Log("Looking for " + IDToFind);
        SceneManager.LoadScene(sceneToChangeTo);


    }
    public void onLevelWasLoaded(Scene scene, LoadSceneMode mode)
    { 
        doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in doors)
        {
            Debug.Log("Looking at " + door);
            if (door.GetComponent<InteractionSceneChange>().myDoor.myID == IDToFind)
            {
                Debug.Log("Found Door with ID " + door.GetComponent<InteractionSceneChange>().myDoor.myID);
                targetGO = door;
                break;
            }
        }
        if (targetGO != null)
        {
            Player.transform.position = targetGO.GetComponent<InteractionSceneChange>().myTransOnChangingScene.position;
        }
        return;
    }
}
