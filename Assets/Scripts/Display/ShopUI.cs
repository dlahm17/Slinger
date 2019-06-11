using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject fullGO;
    ShopSlot[] slots = new ShopSlot[15];
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slots = GetComponentsInChildren<ShopSlot>();
        foreach(ShopSlot slot in slots)
        {
            slot.myShop = this;
        }
    }

    public void buy(Item itemToBuy, int cost)
    {
       float curGold =  Inventory.instance.checkGold();
       if(curGold >= cost)
       {
            if(Inventory.instance.Add(itemToBuy) == true)
            {

                Inventory.instance.giveGold(-cost);
            }

       }
    }
    public void activate()
    {
        Debug.Log("Activating");
        UIController.instance.activateUI("CustomUI");
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void Exit()
    {
        Debug.Log("Exiting");
        player.GetComponent<offlinePlayerMovement>().canMove = true;
        player.GetComponent<offlinePlayerShooting>().canFire = true;
        fullGO.SetActive(false);
        UIController.instance.deactivateUI();
    }
}
