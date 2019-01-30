using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class explosiveMinionCtrl : MonoBehaviour {
    m_Health otherHp;
    float damage = 1f;
    m_Health myHP;


    

    [SerializeField]
    float enragedSpeed;

    bool enraged = false;

    public GameObject explosionToDamagePlayer;

    // Use this for initialization
    void Start()
    {
        myHP = GetComponent<m_Health>();
    }

    private void Update()
    {
        if (!enraged)
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position, transform.forward * 10);
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 1, true);
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    Debug.Log("I SAW THE PLAYER, HE GONNN DIE");
                    NavMeshAgent me = GetComponent<NavMeshAgent>();
                    enemyMoveTowardsPlayer alsoMe = GetComponent<enemyMoveTowardsPlayer>();
                    me.acceleration = 5f;
                    me.speed = enragedSpeed * alsoMe.baseSpeed;
                    alsoMe.resetTarget(hit.collider.gameObject);
                    enraged = true;
                    StartCoroutine("ThreeSecondsTilExplosion");
                }
            }
        }
    }



    private void OnCollisionEnter(Collision other)
    {

        otherHp = other.gameObject.GetComponent<m_Health>();
        if (otherHp != null)
        {
            otherHp.takeDamage(damage, damageType.physical);

        }
        if (other.gameObject.tag.Equals("Player"))
        {
            otherHp.takeDamage(damage, damageType.physical);
            myHP.takeDamage(damage, damageType.physical);

        }
    }

    

    IEnumerator ThreeSecondsTilExplosion()
    {
        yield return new WaitForSeconds(3);
        Instantiate(explosionToDamagePlayer, transform.position, Quaternion.Euler(0,0,0));
        myHP.takeDamage(10000000, damageType.explosive);
    }
}
