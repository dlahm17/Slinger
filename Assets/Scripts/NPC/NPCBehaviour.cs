using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    NavMeshAgent myMovement;
    GameObject NavMeshLocations;



    private void Start()
    {
        myMovement = GetComponent<NavMeshAgent>();
        NavMeshLocations = GameObject.Find("NavMeshHelper");

    }

    private void Update()
    {

    }
}
