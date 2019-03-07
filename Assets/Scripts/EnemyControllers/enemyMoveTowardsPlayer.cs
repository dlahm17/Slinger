using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMoveTowardsPlayer : MonoBehaviour {
    List<GameObject> players;
    GameObject[] PlayerArray = new GameObject[25];
    //string currentMode = "findPlayer";
    NavMeshAgent myAI;
    //Rigidbody myrb;

    public float checkTime;
    public float timeToCheckTarget;

    public float baseSpeed;



    public bool gunEnemy;
    public float radiusToPlayer;

    GameObject target;

    enemyHealth myHealth;
	// Use this for initialization
	void Start () {
        //myrb = GetComponent<Rigidbody>();

        myHealth = GetComponent<enemyHealth>();
        
        players = new List<GameObject>();
        myAI = gameObject.GetComponent<NavMeshAgent>();
        baseSpeed = myAI.speed;
        
       


        getplayers();

        myAI.stoppingDistance = radiusToPlayer;

    }

    public void resetTarget(GameObject targetToSet)
    {
        target = targetToSet;
        return;
    }

    void getplayers()
    {
        PlayerArray = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in PlayerArray)
        {
            players.Add(player);
        }
        setTarget();
    }

    void setTarget()
    {
        float tempDistance = 0;
        float distance;
        //should find the closest player to go after
        foreach (GameObject player in PlayerArray)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if(Mathf.Abs(distance) > Mathf.Abs(tempDistance))
            {
                tempDistance = distance;
                target = player;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        //time to check the target should decrease the closer the enemy is to the player, with it capping at a certain point to prevent bullshit
        
            if (Time.time > checkTime)
            {
            if (!myHealth.amDed)
            {
                myAI.SetDestination(target.transform.position);
                checkTime = Time.time + timeToCheckTarget;
            }


        }
          
    }
}

    

