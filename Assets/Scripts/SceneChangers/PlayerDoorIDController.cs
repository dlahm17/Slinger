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

    Door InteractedDoor;
    Door ConnectedDoor;

    GameObject targetGO;
    GameObject[] doors;

    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += onLevelWasLoaded;
    }
    public void changeTheScene(Door interactedDoor)
    {
        Debug.Log(interactedDoor);
        ConnectedDoor = null;
        targetGO = null;
        doors = null;
        
        string sceneToChangeTo = "Debug";
        
        sceneToChangeTo = interactedDoor.sceneToChangeTo;
        ConnectedDoor = interactedDoor.connectedDoor;
        Debug.Log("Looking for: " + ConnectedDoor);
        SceneManager.LoadScene(sceneToChangeTo);


    }
    public void onLevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        lookForConnectedDoor();
    }

    void lookForConnectedDoor()
    {
        bool foundDoor = false;
        doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in doors)
        {
            Debug.Log("Looking at " + door.GetComponent<InteractionSceneChange>().myDoor);
            if (door.GetComponent<InteractionSceneChange>().myDoor.Equals(ConnectedDoor) && !foundDoor)
            {
                Debug.Log("Found Door: " + door.GetComponent<InteractionSceneChange>().myDoor);
                targetGO = door;
                foundDoor = true;
                break;
            }
        }
        if (targetGO != null)
        {
            Player.transform.position = targetGO.GetComponent<InteractionSceneChange>().myTransOnChangingScene.position;
            Player.GetComponent<offlinePlayerMovement>().resetCamPos();
        }
        if(targetGO == null)
        {
            Debug.Log("Could not find door");
        }
        return;
    }
}
