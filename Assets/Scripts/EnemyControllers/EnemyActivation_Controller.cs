using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation_Controller : MonoBehaviour {
    /// <summary>
    /// EnemyActivationController is the overall controller that gets information put into it and then tells grids to activate and deactivate as appropriate
    /// </summary>
    #region singleton
    public static EnemyActivation_Controller instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of the Enemy Controller found!");
            return;
        }
        instance = this;
    }
    #endregion
    GameObject GridParent;
    EnemyGridController[] Grids;
    EnemyGridController currentActiveGrid;
    
    // Use this for initialization
    void Start () {
        getGrids();
	}
	public void getGrids()
    {

        GridParent = GameObject.Find("GridOrganizer");
        Grids = GridParent.GetComponentsInChildren<EnemyGridController>();
        DeActivateAll();
    }
	public void Activate(EnemyGridController newGrid)
    {
        if (newGrid != null)
        {
            //Debug.Log("Changing active grid to " + newGrid.transform.position);
            EnemyGridController tempGrid = currentActiveGrid;
            currentActiveGrid = newGrid;
            currentActiveGrid.activateGrid();
            DeActivate(tempGrid);
        }
    }
    public void DeActivateAll()
    {
        foreach(EnemyGridController Grid in Grids)
        {
            Grid.deActivate();
        }
    }
    public void DeActivate(EnemyGridController oldGrid)
    {
        if (oldGrid != null)
        {
            oldGrid.deActivate();
        }
    }
    public EnemyGridController getgridInfo()
    {
        return currentActiveGrid;
    }
}
