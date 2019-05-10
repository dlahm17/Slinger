using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    #region singleton
    public static UIController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple UIControllers are in scene");
            return;
        }
        instance = this;
    }
    #endregion
    public GameObject[] PlayerGameplayUI = new GameObject[2];
    public GameObject NodeUI;
    public GameObject DialogueUI;
    public GameObject InventoryUI;
    public GameObject PauseUI;
    public bool active = false;
    public void activateUI(string UIToActivate)
    {
        //Debug.Log("UI Activated");
        switch (UIToActivate)
        {
            case "NodeUI":
                //Debug.Log("Node UI Up");
                foreach (GameObject U in PlayerGameplayUI)
                {
                    U.SetActive(false);
                }
                NodeUI.SetActive(true);
                break;
            case "DialogueUI":
                //Debug.Log("Dialogue UI Up");
                foreach (GameObject U in PlayerGameplayUI)
                {
                    U.SetActive(false);
                }
                DialogueUI.SetActive(true);
                break;
            case "InventoryUI":
                //Debug.Log("Inventory UI Up");
                foreach (GameObject U in PlayerGameplayUI)
                {
                    U.SetActive(false);
                }
                InventoryUI.SetActive(true);
                break;
            case "PauseUI":
                //Debug.Log("Pause UI Up");
                foreach (GameObject U in PlayerGameplayUI)
                {
                    U.SetActive(false);
                }
                PauseUI.SetActive(true);
                break;
            case "CustomUI":
                //Debug.Log("Custom UI up");
                foreach(GameObject U in PlayerGameplayUI)
                {
                    U.SetActive(false);
                }
                break;
            default:
                Debug.LogWarning("Improper call to UIController, see activated UI code to fix it");
                break;
        }
    }
    public void deactivateUI()
    {
        NodeUI.SetActive(false);
        DialogueUI.SetActive(false);
        InventoryUI.SetActive(false);
        PauseUI.SetActive(false);
        foreach (GameObject U in PlayerGameplayUI)
        {
            U.SetActive(true);
        }
        
    }
}
