using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stat { health, armor, damage, magicDamage, magicArmor, Speed, Stealth, unSelected}
public class node : InteractablePickup
{
    public int value;
    public int cost;
    public bool isAvailableForPurchase;
    public bool isPurchased;
    nodeController myCtrl;

    stat myS = stat.unSelected;
    public override void Interact()
    {
        base.Interact();
        myCtrl = nodeController.instance;
        if (isAvailableForPurchase == true && isPurchased == false)
        {
            if (checkEXP() == true)
            {
                sendInformation();
            }
        }
        if(isAvailableForPurchase == true && isPurchased == true && myS == stat.unSelected)
        {
            selectStat();
        }
    }

    public void selectStat()
    {
        myCtrl.spawnUI(this);
    }

    public bool checkEXP()
    {
        if(Inventory.instance.checkExp() > cost)
        {
            //There's enough experience to buy it
            Debug.Log("Enough EXP");
            return true;
        }
        Debug.Log("Not Enough exp");
        return false;
    }

    public void sendInformation()
    {
        myCtrl.spawnUI(this);
    }

    //This is just for visuals
    public void changeStat(stat changed)
    {
        Inventory.instance.giveExp(-cost);
        myS = changed;
        drawLineTwoPoints me = GetComponent<drawLineTwoPoints>();
        me.setActive();
        
    }
}
