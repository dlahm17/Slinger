using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnContactDealDamage_FromPlayer : MonoBehaviour
{
    public playerStats myStats;
    public damageType myType;
    public float damage = 5f;
    Collider myCollider;

    float reloadCollider = 2f;
    float timeToReloadCollider = 2f;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (myCollider.enabled == false && Time.time > reloadCollider)
        {
            myCollider.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            float damageToDeal = damage + myStats.damage.GetValue();
            enemyHealth Ehealth = collision.gameObject.GetComponent<enemyHealth>();
            if (Ehealth != null)
            {
                Ehealth.takeDamage(damageToDeal, myType);
                myCollider.enabled = false;
                reloadCollider = Time.time + timeToReloadCollider;

            }
        }
    }
    
}
