using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    /// <summary>
    /// Enemy Spawn is the individual enemies located within a grid.  it does not communicate up the ladder
    /// Controls Enemy Activation and Deactivation, 
    /// </summary>
    public GameObject EnemyPrefab;
    GameObject currentEnemy;

    bool hasSpawned = false;
        
    public void Deactivate()
    {
        if (currentEnemy == null && hasSpawned == false)
        {
            hasSpawned = true;
            currentEnemy = Instantiate(EnemyPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            currentEnemy.transform.position = transform.position;
            currentEnemy.SetActive(false);
            return;
        }
        if (currentEnemy != null)
        {
            currentEnemy.SetActive(false);
            return;
        }
    }
    public void Activate()
    {
        if (currentEnemy != null)
        {
            currentEnemy.SetActive(true);
        }
        if(currentEnemy == null && hasSpawned == false)
        {
            Deactivate();
            Activate();
        }
    }
        
}
