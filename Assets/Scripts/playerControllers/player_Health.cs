using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Health : m_Health
{
    playerStats playerstats;
    public int CurrentExp;
    EquipmentManager hpManage;
    // Start is called before the first frame update
    public override void Start()
    {
        hpManage = EquipmentManager.instance;
        hpManage.onEquipmentChanged += changeHP;
        
        playerstats = GetComponent<playerStats>();
        health = playerstats.health.GetValue();
        setHPBarValues(playerstats.health.GetValue(), playerstats.health.GetValue());
        setName(myName);
    }

    void changeHP(Equipment newItem, Equipment oldItem)
    {
        setHPBarValues(playerstats.health.GetValue(), health);
    }

    public override void takeDamage(float damage, damageType damageTyping)
    {
        float damageToTake = damage - playerstats.armor.GetValue();
        if(damageToTake < 0)
        {
            damageToTake = 0;
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
