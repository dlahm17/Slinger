using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnContactStayStill : MonoBehaviour
{
    public bool isIcycle = false;
    Rigidbody myrb;

    bool stuck = false;
    Transform target;

    private void Awake()
    {
        myrb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isIcycle == true)
        {
            GetComponent<OnContactDealDamage>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
        myrb.isKinematic = true;
        myrb.velocity = Vector3.zero;

        target = collision.transform;
        gameObject.transform.SetParent(target);
        
    }
}
