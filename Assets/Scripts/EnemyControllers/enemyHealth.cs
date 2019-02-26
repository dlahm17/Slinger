using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : m_Health
{
    //Derives from the overall health class therefore all the forms for taking damage are in play.
    public bool deathByExplosion;
    public GameObject Explosion;

    public bool deathByAudio;
    public AudioSource deathAudio;

    public bool deathWithAnim;
    public Animator myAnim;
    public float timeForAnimation = 0f;


    public float Armor = 5f;
    public float magicalArmor = 5f;

    public override void takeDamage(float damage, damageType damageTyping)
    {
        Debug.Log("Taking Damage in enemy health");
        float damageToTake = damage;
        if(damageTyping == damageType.physical)
        {
            damageToTake = damage - Armor;
            if(damageToTake <= 0)
            {
                damageToTake = 1;
            }

        }
        if(damageTyping != damageType.physical)
        {
            //input everything magical, and non physical here.
        }
        health -= damageToTake;
        if(health <= 0 && amDed == false)
        {
            amDed = true;
            death();
            health = 0;
        }

    }
    public override void death()
    {
        base.death();
        if (deathByExplosion)
        {
            Explosion.SetActive(true);
            
        }
        if (deathByAudio)
        {
            deathAudio.Play();
        }
        if (deathWithAnim)
        {
            if (myAnim != null)
            {
                myAnim.SetBool("Dead", true);
            }
        }
        StartCoroutine("waitForAnimFinish");

    }
    IEnumerator waitForAnimFinish()
    {
        yield return new WaitForSeconds(timeForAnimation);
        Destroy(gameObject);
    }
}
