using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunEnemyCtrl : MonoBehaviour {
    m_Health otherHp;
    float damage;
    m_Health myHP;

    public offlineWaveSpawning myofflineSpawner;
    public float range;
    public LayerMask playerLayer;

    public GameObject projectile;
    bulletForce myBullet;
    GameObject bulletISpawn;
    public Transform gunPos;

    float reloadTime = 1f;
    float timeToReload = 1f;
    // Use this for initialization
    void Start()
    {
        myHP = GetComponent<m_Health>();
    }

    private void Update()
    {
        
        Debug.DrawRay(gunPos.position, transform.forward * range, Color.red, 1f, true);
        RaycastHit hit;
        Ray ray = new Ray(gunPos.position, transform.forward * range);
        if (Time.time > reloadTime)
        {
            if(Physics.Raycast(ray, out hit, playerLayer))
            {
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    Debug.Log("Firing on player");
                    shoot();
                    reloadTime = Time.time + timeToReload;
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

    void shoot()
    {
        bulletISpawn  = Instantiate(projectile, gunPos.position, Quaternion.Euler(0, gameObject.transform.rotation.y, 0));
        bulletISpawn.transform.rotation = gameObject.transform.rotation;
        myBullet = bulletISpawn.GetComponent<bulletForce>();
        myBullet.origin = gameObject;
    }
}
