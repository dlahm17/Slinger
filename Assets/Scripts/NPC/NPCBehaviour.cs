using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    NavMeshAgent myMovement;
    GameObject NavMeshLocations;

    Transform[] meshLocations;



    private void Start()
    {
        myMovement = GetComponent<NavMeshAgent>();
        NavMeshLocations = GameObject.Find("NavMeshHelper");
        meshLocations = NavMeshLocations.GetComponentsInChildren<Transform>();

    }

    private void Update()
    {

    }
}
