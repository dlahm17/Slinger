using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Davis L
/// Purpose: to derive an enemy specific health from the main health module.  enemy health will be specific for all enemies used in the game, including bosses
/// </summary>
public class enemyHealth : m_Health
{

    //Derives from the overall health class therefore all the forms for taking damage are in play.
    EnemyUIController UI;

    //Death by explosion explains if there will be an explosion in the death scene
    public bool deathByExplosion;
    //if deathbyexplosion is used then we need an explosion object for this.
    public GameObject Explosion;

    //death by audio explains if there will be audio in the death scene
    public bool deathByAudio;
    //death audio is the audiosource that will be used by the death
    public AudioSource deathAudio;

    //death with anim explains if there will be a specific animation used by the enemy
    public bool deathWithAnim;
    //myanim is the animation used by the enemy upon dying
    public Animator myAnim;
    //time for animation is the amount of time it will take for the enemy to die.  That way full destruction can take place only after the animation is done.
    public float timeForAnimation = 0f;

    //exp to give is the amount of experience the player will recieve for absorbing this enemy.  Base value is 5, though that is modifiable.
    public int ExpToGive = 5;

    //goldtoDrop is a gold bag gameobject the enemy may spawn upon dying.
    public GameObject goldToDrop;

    //armor and magical armor is the enemy's defenses against physical and magical attacks, the higher these values are, the less damage they will take per attack.
    public float Armor = 5f;
    public float magicalArmor = 5f;

    public override void Start()
    {
        base.Start();
        //make sure to grab the enemy UI controller so we can push the health bar to the ui controller.
        UI = EnemyUIController.instance;
    }

    //Takedamage overrides the m_health takedamage since they'll also be receiving specific damage types.
    public override void takeDamage(float damage, damageType damageTyping)
    {
        //Debug.Log("Taking Damage in enemy health");
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
            //input everything magical, and non physical here.  Note this is not done yet.
        }
        //After damage has been reduced by armor, magic armor, and other phenomenon, we can remove the health the damage is taking.
        health -= damageToTake;
        //if health is less than or equal to 0, we can call the death function
        if(health <= 0 && amDed == false)
        {
            amDed = true;
            death();
            health = 0;
        }
        if (health >= 0)
        {
            UI.setUI(myName, health, maxhp);
        }
    }
    //isDead is a query function other scripts can use to ask if this enemy is dead.
    public bool isDead()
    {
        return amDed;
    }
    //Death handles what will result when the enemy reaches 0 in the healthbar.
    public override void death()
    {
        base.death();
        UI.deactivateUI();
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
        UI.deactivateUI();
        Destroy(gameObject);
    }
}
