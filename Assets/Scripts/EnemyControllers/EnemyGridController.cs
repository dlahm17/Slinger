using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGridController : MonoBehaviour {
    /// <summary>
    /// Enemy Grid Controller is the Individual Grids scattered across the world.  Each Grid contains a series of spawners that control the active nature of Enemies.
    /// Upon Activation from the EnemyActivation Controller it'll call activate across all the spawners.
    /// Enemy Grid Controller also determines if the player is in range, and will call the activation script in the controller.
    /// </summary>
    EnemySpawn[] enemySpawn;
    BoxCollider myCollider;
    EnemyActivation_Controller myCtrl;
    offlinePlayerMovement playCtrl;
    //Radius is the size of the hitbox of the grid in question
    public float radiusX;
    public float radiusY;
    public float radiusZ;
    //Buffer is the distance from the edge of the grid to where the player will stop in transition
    public float bufferX;
    public float BufferZ;


    public Transform bufX;
    public Transform bufZ;
    public Transform minBufX;
    public Transform minBufZ;
    /// <summary>
    /// EnemyGridController is an individual grid that activates enemies within its radius
    /// It should also contain all the information to make transitions smooth for the player
    /// </summary>
    // Use this for initialization
    void Start() {
        //This grabs the movement script from the player, and the enemySpawners that are within the grid.
        playCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<offlinePlayerMovement>();
        enemySpawn = gameObject.GetComponentsInChildren<EnemySpawn>();
        //The collider that is attached to this gameobject
        myCollider = GetComponent<BoxCollider>();
        if(myCollider != null)
        {
            //This sets the collider equal to the radiuses that are set in the editor.
            myCollider.size = new Vector3(radiusX, radiusY, radiusZ);
        }
        myCtrl = EnemyActivation_Controller.instance;
        foreach (EnemySpawn Enemyspawn in enemySpawn)
        {
            //We then deactivate all the enemies in the spawnradius.
            Enemyspawn.Deactivate();
        }

        //These positions are for debugging and they also have their information be sent to the overall controller.
        bufX.localPosition = new Vector3((radiusX / 2 - bufferX / 2), 0, 0);
        minBufX.localPosition = new Vector3(((-radiusX / 2)) + (bufferX / 2), 0, 0);
        bufZ.localPosition = new Vector3(0, 0, ((radiusZ / 2)) - (BufferZ));
        minBufZ.localPosition = new Vector3(0, 0, ((-radiusZ / 2)) + (BufferZ / 2));

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(radiusX,radiusY,radiusZ));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(radiusX - bufferX, radiusY, radiusZ - BufferZ));
    }
    public void activateGrid()
    {
        //upon activating it'll activate all the enemies within it.
        foreach(EnemySpawn Enemyspawn in enemySpawn)
        {
            Enemyspawn.Activate();
        }
    }
    public void deActivate()
    {
        if (enemySpawn != null)
        {
            foreach (EnemySpawn Enemyspawn in enemySpawn)
            {
                Enemyspawn.Deactivate();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playCtrl.changeGridAnim(this);
            myCtrl.Activate(this);
        }
    }
}
