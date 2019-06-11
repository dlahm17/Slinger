using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCtrl : Enemy {
    public float damage;
    public EnemyWpnHitbox myHitBox;
    public MeshCollider hitBox;
    public float attackDistanceFromPlayer = 2f;
    GameObject player;
    bool currentlyAttacking = false;
    float bufferUpdate = .2f;
    float timeToUpdateMovement;
    Animator myAnim;


    public float rotationTracking = .5f;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        timeToUpdateMovement = Time.time + bufferUpdate;
        myAnim = GetComponent<Animator>();


        myHitBox.enabled = true;
    }

    void Update()
    {
        if (myState == enemyState.combat)
        {
            determineMovement();
            if (currentlyAttacking)
            {
                Vector3 dir = player.transform.position - transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationTracking * Time.deltaTime);
            }
        }
    }
    void determineMovement()
    {
        float curDistFromPlayer;
        curDistFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (curDistFromPlayer <= attackDistanceFromPlayer && currentlyAttacking == false)
        {
            attack();
        }
        if(curDistFromPlayer > attackDistanceFromPlayer && Time.time > timeToUpdateMovement && currentlyAttacking == false)
        {
            myMovement.setDestination(player.transform.position);
            timeToUpdateMovement = Time.time + bufferUpdate;
        }

    }
    void attack()
    {
        currentlyAttacking = true;
        myAnim.SetBool("Attacking", true);

        StartCoroutine("endAttackAnim");
    }
    IEnumerator endAttackAnim()
    {
        myMovement.myAgent.isStopped = true;
        yield return new WaitForSeconds(.5f);
        myHitBox.enabled = true;
        yield return new WaitForSeconds(1.25f);
        myHitBox.enabled = false;
        myAnim.SetBool("Attacking", false);
        yield return new WaitForSeconds(1f);
        currentlyAttacking = false;
        myMovement.myAgent.isStopped = false;
    }
}
