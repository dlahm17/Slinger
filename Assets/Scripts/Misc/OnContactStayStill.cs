using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnContactStayStill : MonoBehaviour
{
    public bool isDamaging = false;
    Rigidbody myrb;

    bool stuck = false;
    public Transform target;

    private void Awake()
    {
        myrb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (stuck == false)
        {
            if (isDamaging == true)
            {
                GetComponent<OnContactDealDamage>().enabled = false;
                GetComponent<Collider>().enabled = false;

                GetComponent<destroyInSeconds>().enabled = true;
                GetComponent<destroyInSeconds>().setTime();
            }

            myrb.constraints = RigidbodyConstraints.FreezePositionX;
            myrb.constraints = RigidbodyConstraints.FreezePositionY;
            myrb.constraints = RigidbodyConstraints.FreezePositionZ;
            myrb.velocity = Vector3.zero;

            target.transform.SetParent(collision.gameObject.transform);

            stuck = true;
        }
    }
    private void Update()
    {
        if (stuck)
        {
            if (target != null)
            {
                gameObject.transform.position = target.transform.position;
                gameObject.transform.rotation = target.transform.rotation;
            }
        }
    }
}
