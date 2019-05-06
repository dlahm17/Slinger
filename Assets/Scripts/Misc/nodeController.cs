using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeController : MonoBehaviour
{
    //Node Controller is the controller for talking to stats, and sending information down and back to nodes.
    public static nodeController instance;
    #region singleton
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one node controller in scene");
            return;
        }
        instance = this;
    }
    #endregion
    
    node currentlySelectedNode;
    public GameObject UI;
    GameObject player;
    public void Start()
    {
       
        UI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void spawnUI(node n)
    {
        Debug.Log("Spawning UI");
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
        UIController.instance.activateUI("NodeUI");
        currentlySelectedNode = n;
    }
    public void HPSelect()
    {
        UIController.instance.deactivateUI();
        playerStats pst = player.GetComponent<playerStats>();
        currentlySelectedNode.changeStat(stat.health);
        pst.health.addModifier(currentlySelectedNode.value);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void DMGSelect()
    {
        UIController.instance.deactivateUI();
        playerStats pst = player.GetComponent<playerStats>();
        currentlySelectedNode.changeStat(stat.damage);
        pst.damage.addModifier(currentlySelectedNode.value);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void MagDmgSelect()
    {
        UIController.instance.deactivateUI();
        playerStats pst = player.GetComponent<playerStats>();
        currentlySelectedNode.changeStat(stat.magicDamage);
        pst.magicDamage.addModifier(currentlySelectedNode.value);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void ArmorSelect()
    {
        UIController.instance.deactivateUI();
        playerStats pst = player.GetComponent<playerStats>();
        currentlySelectedNode.changeStat(stat.armor);
        pst.armor.addModifier(currentlySelectedNode.value);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void MagArmorSelect()
    {
        UIController.instance.deactivateUI();
        playerStats pst = player.GetComponent<playerStats>();
        currentlySelectedNode.changeStat(stat.magicArmor);
        pst.magicArmor.addModifier(currentlySelectedNode.value);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void SpdSelect()
    {
        UIController.instance.deactivateUI();
        playerStats pst = player.GetComponent<playerStats>();
        currentlySelectedNode.changeStat(stat.Speed);
        pst.speed.addModifier(currentlySelectedNode.value);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void StlthSelect()
    {
        UIController.instance.deactivateUI();
        playerStats pst = player.GetComponent<playerStats>();
        currentlySelectedNode.changeStat(stat.Stealth);
        pst.stealth.addModifier(currentlySelectedNode.value);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void unSelected()
    {
        UIController.instance.deactivateUI();
        currentlySelectedNode.changeStat(stat.unSelected);
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }

}
