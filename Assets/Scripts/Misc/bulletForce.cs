using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletForce : MonoBehaviour {
    public float speed;
    m_Health otherHp;
    public float bulletDamage = 1;
    public float forcemultiplier = 1;

    public GameObject origin;
    
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, 0, (1 * speed) * Time.deltaTime), Space.Self);
	}


    private void OnCollisionEnter(Collision other)
    {

       
        if (other.gameObject.tag.Equals("Cover") || other.gameObject.tag.Equals("Floor"))
        {

            Destroy(gameObject);
        }
        if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Enemy"))
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                otherHp = other.gameObject.GetComponent<player_Health>();
                if (otherHp != null)
                {
                    if (other.gameObject != origin)
                    {
                        //create the knockback force
                        Vector3 force;
                        //set the force for the applicable direction from the player.  and then we divide the force by the armor value of the player.  Armor value will reduce knockback.
                        force = other.gameObject.transform.position - transform.position;

                        force = force / other.gameObject.GetComponent<playerStats>().armor.GetValue();
                        //and multiply it by the forcemultiplier of the attack.
                        force.y = 0;
                        other.gameObject.GetComponent<Rigidbody>().AddForce(force * forcemultiplier);
                        //We then apply bullet damage
                        otherHp.takeDamage(bulletDamage, damageType.physical);
                    }
                }
            }
            if (other.gameObject.tag.Equals("Enemy"))
            {
                otherHp = other.gameObject.GetComponent<enemyHealth>();
                if (otherHp != null)
                {
                    if (other.gameObject != origin)
                    {
                        otherHp.takeDamage(bulletDamage, damageType.physical);
                    }
                }
            }
            if (other.gameObject != origin)
            {
                Destroy(gameObject);
            }
        }
    }
}
