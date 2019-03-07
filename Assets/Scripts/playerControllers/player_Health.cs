using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_Health : m_Health
{
    playerStats playerstats;
    public int CurrentExp;
    EquipmentManager hpManage;

    float storedDamage = 0f;
    bool fireActive;
    float timeForfire;
    bool acidActive;
    float timeForAcid;
    bool poison;
    float timeForPoison;


    public GameObject OBJpoisonSlider;
    public GameObject OBJacidSlider;
    public GameObject OBJfireSlider;
    Slider poisonSlider;
    Slider acidSlider;
    Slider fireSlider;

    float timeToTakeDmg = .2f;
    float reloadTimeToTakeDmg = .2f;

    // Start is called before the first frame update
    public override void Start()
    {
        poisonSlider = OBJpoisonSlider.GetComponent<Slider>();
        acidSlider = OBJacidSlider.GetComponent<Slider>();
        fireSlider = OBJfireSlider.GetComponent<Slider>();
        OBJpoisonSlider.SetActive(false);
        OBJacidSlider.SetActive(false);
        OBJfireSlider.SetActive(false);
        fireActive = false;
        acidActive = false;
        poison = false;
        hpManage = EquipmentManager.instance;
        hpManage.onEquipmentChanged += changeHP;
        
        
        playerstats = GetComponent<playerStats>();
        health = playerstats.health.GetValue();
        setHPBarValues(playerstats.health.GetValue(), playerstats.health.GetValue());
        setName(myName);
    }

    private void FixedUpdate()
    {
        if (fireActive)
        {
            if (Time.time > timeToTakeDmg)
            {
                takeDamage(storedDamage, damageType.fire);
                fireSlider.value = Time.time;
                if (Time.time > timeForfire)
                {
                    fireActive = false;
                    OBJfireSlider.SetActive(false);
                    storedDamage = 0;
                }
                timeToTakeDmg = Time.time + reloadTimeToTakeDmg;
            }
        }
        if (acidActive)
        {
            if (Time.time > timeToTakeDmg)
            {
                takeDamage(storedDamage, damageType.acid);
                acidSlider.value = Time.time;
                if (Time.time > timeForAcid)
                {
                    acidActive = false;
                    OBJacidSlider.SetActive(false);
                    storedDamage = 0;
                }
                timeToTakeDmg = Time.time + reloadTimeToTakeDmg;
            }
        }
        if (poison)
        {
            if (Time.time > timeToTakeDmg)
            {
                takeDamage(storedDamage, damageType.poison);
                poisonSlider.value = Time.time;
                if (Time.time > timeForPoison)
                {
                    poison = false;
                    OBJpoisonSlider.SetActive(false);
                    storedDamage = 0;
                }
                timeToTakeDmg = Time.time + reloadTimeToTakeDmg;
            }
        }
    }

    void changeHP(Equipment newItem, Equipment oldItem)
    {
        setHPBarValues(playerstats.health.GetValue(), health);
    }

    public override void takeDamage(float damage, damageType damageTyping)
    {
        Debug.Log("Taking Damage to Player");
        float damageToTake = damage;
        if (damageTyping == damageType.fire)
        {
            damageToTake -= playerstats.armor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }
            if (!fireActive)
            {
                timeForfire = Time.time + damageToTake;
                storedDamage = damageToTake / 2;
                fireSlider.maxValue = timeForfire;
                fireSlider.minValue = Time.time;
                OBJfireSlider.SetActive(true);
                fireActive = true;
            }
        }
        if (damageTyping == damageType.acid)
        {
            damageToTake -= playerstats.magicArmor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }
            if (!acidActive)
            {
                timeForAcid = Time.time + damageToTake;
                storedDamage = damageToTake / 2;
                acidSlider.maxValue = timeForAcid;
                acidSlider.minValue = Time.time;
                OBJacidSlider.SetActive(true);
                acidActive = true;
            }
        }
        if (damageTyping == damageType.electricity)
        {
            damageToTake -= playerstats.magicArmor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }
        }
        if (damageTyping == damageType.explosive)
        {
            damageToTake -= playerstats.armor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }
        }
        if (damageTyping == damageType.ice)
        {
            damageToTake -= playerstats.armor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }
        }
        if (damageTyping == damageType.magical)
        {
            damageToTake -= playerstats.magicArmor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }
        }
        if (damageTyping == damageType.physical)
        {
            damageToTake-= playerstats.armor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }

        }
        if (damageTyping == damageType.poison)
        {
            damageToTake -= playerstats.magicArmor.GetValue();
            if (damageToTake < 0)
            {
                damageToTake = 0;
            }
            if (!poison)
            {
                timeForPoison = Time.time + damageToTake;
                storedDamage = damageToTake / 2;
                poisonSlider.maxValue = timeForPoison;
                poisonSlider.minValue = Time.time;
                OBJpoisonSlider.SetActive(true);
                poison = true;
            }
        }







        health -= damageToTake;


        if(health <= 0)
        {
            death();
        }

    }
    public override void death()
    {
        base.death();
        

    }
}
