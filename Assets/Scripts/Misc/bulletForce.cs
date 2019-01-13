using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletForce : MonoBehaviour {
    public float speed;
    m_Health otherHp;
    public float bulletDamage = 1;

    public GameObject origin;
    
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, 0, (1 * speed) * Time.deltaTime), Space.Self);
	}


    private void OnCollisionEnter(Collision other)
    {

        otherHp = other.gameObject.GetComponent<m_Health>();
        if (otherHp != null)
        {
            if (other.gameObject != origin)
            {
                otherHp.takeDamage(bulletDamage, damageType.physical);
            }
        }
        if (other.gameObject.tag.Equals("Cover") || other.gameObject.tag.Equals("Floor"))
        {

            Destroy(gameObject);
        }
        if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Enemy"))
        {
            if (other.gameObject != origin)
            {
                Destroy(gameObject);
            }
        }
    }
}
