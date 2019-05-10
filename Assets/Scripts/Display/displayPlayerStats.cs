using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class displayPlayerStats : MonoBehaviour
{
    playerStats myStats;
    public Text playStats_Health;
    public Text playStats_Damage;
    public Text playStats_MagicDamage;
    public Text playStats_Armor;
    public Text playStats_MagicArmor;
    public Text playStats_Speed;
    private void Start()
    {
        myStats = GetComponent<playerStats>();
        playStats_Health.text = myStats.health.GetValue().ToString();
        playStats_Damage.text = myStats.damage.GetValue().ToString();
        playStats_MagicDamage.text = myStats.magicDamage.GetValue().ToString();
        playStats_Armor.text = myStats.armor.GetValue().ToString();
        playStats_MagicArmor.text = myStats.magicArmor.GetValue().ToString();
        playStats_Speed.text = myStats.speed.GetValue().ToString();
    }

    public void onChange()
    {
        playStats_Health.text = myStats.health.GetValue().ToString();
        playStats_Damage.text = myStats.damage.GetValue().ToString();
        playStats_MagicDamage.text = myStats.magicDamage.GetValue().ToString();
        playStats_Armor.text = myStats.armor.GetValue().ToString();
        playStats_MagicArmor.text = myStats.magicArmor.GetValue().ToString();
        playStats_Speed.text = myStats.speed.GetValue().ToString();
    }
}
