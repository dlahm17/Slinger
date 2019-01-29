using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onContactTakeDamage : MonoBehaviour
{
    public damageType myTyping;
    public float damage = 5f;
    public void OnCollisionEnter(Collision collision)
    {
        m_Health contacthealth = collision.gameObject.GetComponent<m_Health>();
        if(contacthealth != null)
        {
            contacthealth.takeDamage(damage, myTyping);
        }
        

        
    }
}
