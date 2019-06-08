using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWpnHitbox : MonoBehaviour
{
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player_Health health = collision.gameObject.GetComponent<player_Health>();
            if (health != null)
            {
                health.takeDamage(damage, myType);
                myCollider.enabled = false;
                reloadCollider = Time.time + timeToReloadCollider;
            }
        }
    }
}
