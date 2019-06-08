using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Enemy movement is the motor function for enemies.  This will be affected by the controllers on the enemies, and have the destinations set from there.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
   public NavMeshAgent myAgent;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }
    public void setDestination(Vector3 destination)
    {
        //Debug.Log("Setting destination to: " + destination);
        myAgent.SetDestination(destination);
    }
}
