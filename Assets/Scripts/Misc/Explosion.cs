using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce = 100f;
    public float upwardsMod = 10f;
    public float fuseTime = 5f;
    public float explosionRadius = 6f;
    public float damage = 25f;
    public damageType myTyping;
    float aliveTime;
    public GameObject ExplosionEffects;
    public LayerMask affectedLayer;

    GameObject[] boomBox;
    private void Start()
    {
        aliveTime = Time.time + fuseTime;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > aliveTime)
        {
            Instantiate(ExplosionEffects, transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, affectedLayer);
            foreach(Collider c in hitColliders)
            {
                player_Health pHp = c.GetComponent<player_Health>();
                if(pHp != null)
                {
                    pHp.takeDamage(damage, myTyping);
                }
                enemyHealth E = c.GetComponent<enemyHealth>();
                if(E != null)
                {
                    E.takeDamage(damage, myTyping);
                }
                Rigidbody r = c.GetComponent<Rigidbody>();
                if(r != null)
                {
                    r.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsMod);
                }
            }
            Destroy(gameObject);
        }
    }
}
