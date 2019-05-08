using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum damageType { physical, poison, fire, electricity, ice, acid, magical, explosive}

public class m_Health : MonoBehaviour {


    /// <summary>
    /// M_Health is a health module that can be attached to any object with a collider so that it can die.
    /// There should be derivative health modules for NPC, Player, Enemy, and Boss Health
    /// </summary>
    public float health;

    public float maxhp;
    public bool amDed = false;
    
    public Slider hpSlider;




    public string myName;
    public Text myTxt;


    
    public virtual void Start()
    {
        //Start just uses debug values, these should be overwritten by the actual scripts.
        maxhp = health;
        setHPBarValues(maxhp, health);
        setName(myName);
        
    }
    public void setHPBarValues(float barMaxHP, float currentHP)
    {
        hpSlider.maxValue = barMaxHP;
        hpSlider.value = currentHP;
    }

    public void setName(string nameToSet)
    {
        myTxt.text = nameToSet;
    }


    //Update is only used to smoothly show the healthbar going down
    private void Update()
    {
        
        if (hpSlider.value > health)
        {
            if (health > 0)
            {
                hpSlider.value -= .1f;
            }
            if(health <= 0)
            {
                hpSlider.value = 0;
            }

         }
        if (hpSlider.value < health)
        {
                hpSlider.value += .1f;
        }
    }
    //heal and takeDamage cause the health to go down, and under takeDamage, it also asks if the object has been killed
    public void heal(float damageToHeal)
    {
        health += damageToHeal;
        if(health > maxhp)
        {
            health = maxhp;
        }
    }
    


    public virtual void takeDamage(float damage, damageType damageTyping)
    {
        //Debug.Log("Taking Damage");
        

        health -= damage;

        if(health <= 0 && amDed == false){
            amDed = true;
            death();
            health = 0;
        }
    }

    //Death is a virtual void because death will be different for npcs, the player, and others
    public virtual void death()
    {
        //as of right now it'll just contain a debug log to explain that something died
        Debug.Log(myName + " Died");
       
    }

    
    
}
