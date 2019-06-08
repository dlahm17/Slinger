using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunEnemyCtrl : Enemy {
    float damage = 1f;
   enemyHealth myHP;
    
    public float range;
    public LayerMask playerLayer;

    float distancetoPlayer;
    public float desiredDistanceFromPlayer;

    public GameObject projectile;
    bulletForce myBullet;
    GameObject bulletISpawn;
    public Transform gunPos;

    float reloadTime = 1f;
    float timeToReload = 1f;
    GameObject player;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        myHP = GetComponent<enemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (myState == enemyState.combat)
        {
            distancetoPlayer = Vector3.Distance(transform.position, player.transform.position);
            if(myMovement.myAgent.hasPath == false && distancetoPlayer < desiredDistanceFromPlayer)
            {
                Debug.Log("time to move");
                //if the player is to the right and above the enemy, we tell the enemy to go farther down and left.
                if(player.transform.position.x > transform.position.x)
                {
                    if(player.transform.position.z > transform.position.z)
                    {
                        Debug.Log("Player is to my right, and above me");
                        myMovement.setDestination(new Vector3(player.transform.position.x - desiredDistanceFromPlayer, player.transform.position.y, player.transform.position.z - desiredDistanceFromPlayer));
                    }
                    if(player.transform.position.z < transform.position.z)
                    {
                        Debug.Log("Player is to my right, and below me");
                        myMovement.setDestination(new Vector3(player.transform.position.x - desiredDistanceFromPlayer, player.transform.position.y, player.transform.position.z + desiredDistanceFromPlayer));
                    }
                }
                if(player.transform.position.x < transform.position.x)
                {
                    if(player.transform.position.z > transform.position.z)
                    {
                        Debug.Log("Player is to my left, and above me");
                        myMovement.setDestination(new Vector3(player.transform.position.x + desiredDistanceFromPlayer, player.transform.position.y, player.transform.position.z - desiredDistanceFromPlayer));
                    }
                    if(player.transform.position.z < transform.position.z)
                    {
                        Debug.Log("Player is to my left and below me");
                        myMovement.setDestination(new Vector3(player.transform.position.x + desiredDistanceFromPlayer, player.transform.position.y, player.transform.position.z + desiredDistanceFromPlayer));
                    }
                }
            }
            if (myMovement.myAgent.hasPath == false && distancetoPlayer >= desiredDistanceFromPlayer)
            {
                Debug.Log("Let's rotate bois");
                Vector3 dir = player.transform.position - transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, 2f * Time.deltaTime);
                Debug.DrawRay(gunPos.position, transform.forward * range, Color.red, 1f, true);
                RaycastHit hit;
                Ray ray = new Ray(gunPos.position, transform.forward * range);
                if (Time.time > reloadTime)
                {

                    if (Physics.Raycast(ray, out hit, playerLayer))
                    {
                        if (!myHP.amDed)
                        {
                            //Debug.Log(hit.collider.gameObject.name);
                            if (hit.collider.gameObject.tag.Equals("Player"))
                            {
                                //Debug.Log("Firing on player");
                                shoot();
                                reloadTime = Time.time + timeToReload;
                            }
                        }
                    }
                }
            }
        }
    }
    

    void shoot()
    {
        bulletISpawn  = Instantiate(projectile, gunPos.position, Quaternion.Euler(0, gameObject.transform.rotation.y, 0));
        bulletISpawn.transform.rotation = gameObject.transform.rotation;
        myBullet = bulletISpawn.GetComponent<bulletForce>();
        myBullet.origin = gameObject;
    }
}
