using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWpnHitbox : MonoBehaviour
{
    public damageType myType;
    public float damage = 5f;
    

    private void Start()
    {
        Debug.Log("enemy hitbox available");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("HitPlayer");
            player_Health health = collision.gameObject.GetComponent<player_Health>();
            if (health != null)
            {
                health.takeDamage(damage, myType);
            }
        }
    }

}
