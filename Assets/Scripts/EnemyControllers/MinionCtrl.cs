using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCtrl : MonoBehaviour {
    m_Health otherHp;
    public float damage = 1f;
    m_Health myHP;
    public offlineWaveSpawning myofflineSpawner;
	// Use this for initialization
	void Start () {
        myHP = GetComponent<m_Health>();
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
            if (myHP != null)
            {
                myHP.takeDamage(damage, damageType.physical);
            }
            
        }
    }
}
