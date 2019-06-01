using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stat { health, armor, damage, magicDamage, magicArmor, Speed, Stealth, unSelected}
public class node : InteractablePickup
{
    public int id;
    public int value;
    public int cost;
    public bool isAvailableForPurchase;
    public bool isPurchased;
    nodeController myCtrl;
    NodeData myDat;
    public Material Act_Mat;
    GameData currentData;

    stat myS = stat.unSelected;
    public override void Start()
    {
        base.Start();
        loadData();
        
    }
    public void loadData()
    {
        currentData = DataSave_Load.instance.GDat;
        bool amIBought = currentData.node_Bought[id].bought;
        if (amIBought == false)
        {
            return;
        }

        if (amIBought == true)
        {
            if (currentData.node_Bought[id].nameID != id.ToString())
            {
                Debug.LogWarning("node data id doesn't match id on node using it.  Node number: " + id.ToString());
            }
            myDat = currentData.node_Bought[id];
            setStats();
        }
    }
    private void setStats()
    {
        myS = myDat.assignedStat;
        drawLineTwoPoints me = GetComponent<drawLineTwoPoints>();
        me.setActive();
        me.haveBought = true;
        isPurchased = true;
        MeshRenderer[] myRender = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer R in myRender)
        {
            R.materials[1] = Act_Mat;
        }
        if (myS != stat.unSelected)
        {
            if (myS == stat.health)
            {
                playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
                pst.health.addModifier(value);
                pst.updateUI();
                GameObject.FindGameObjectWithTag("Player").GetComponent<player_Health>().setHPBarValues(pst.health.GetValue(), GameObject.FindGameObjectWithTag("Player").GetComponent<player_Health>().health);
            }
            if (myS == stat.damage)
            {
                playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
                pst.damage.addModifier(value);
                pst.updateUI();
            }
            if (myS == stat.magicDamage)
            {
                playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
                pst.magicDamage.addModifier(value);
                pst.updateUI();
            }
            if (myS == stat.armor)
            {
                playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
                pst.armor.addModifier(value);
                pst.updateUI();
            }
            if (myS == stat.magicArmor)
            {
                playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
                pst.magicArmor.addModifier(value);
                pst.updateUI();
            }
            if (myS == stat.Speed)
            {
                playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
                pst.speed.addModifier(value);
                pst.updateUI();
            }
            if (myS == stat.Stealth)
            {
                playerStats pst = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
                pst.stealth.addModifier(value);
                pst.updateUI();
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        myCtrl = nodeController.instance;
        bool canBuy = checkEXP();
        if (canBuy)
        {
            if (isAvailableForPurchase == true && isPurchased == false)
            {
                sendInformation();
            }
            if (isAvailableForPurchase == true && isPurchased == false && myS == stat.unSelected)
            {
                selectStat();
            }
        }
    }

    public void selectStat()
    {
        myCtrl.spawnUI(this);
    }

    public bool checkEXP()
    {
        if(Inventory.instance.checkExp() >= cost)
        {
            //There's enough experience to buy it
            return true;
        }
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
        me.haveBought = true;
        isPurchased = true;
        MeshRenderer[] myRender = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer R in myRender)
        {
          R.materials[1] = Act_Mat;
        }

        saveStats();
    }
    void saveStats()
    {
        Debug.Log("Sending stat change to game data");
        currentData.node_Bought[id].bought = true;
        currentData.node_Bought[id].nameID = id.ToString();
        currentData.node_Bought[id].assignedStat = myS;
    }
}
