using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_TP_Nodes : MonoBehaviour
{
    // Start is called before the first frame update
    public NPCDialogueNode preActiveNode;
    public Transform SerenitySpot;
    public Transform NodeSpot;
    public GameObject choiceUI;
    GameObject player;
    public bool isSerenityWitch = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void activate()
    {
        preActiveNode.enabled = false;
        choiceUI.SetActive(true);
        UIController.instance.activateUI("CustomUI");
        player.GetComponent<offlinePlayerMovement>().canMove = false;
        player.GetComponent<offlinePlayerShooting>().canFire = false;
    }
    public void selectYes()
    {
        choiceUI.SetActive(false);
        FadeOut.instance.BeginFade(1);
        StartCoroutine("WaitForFade");
        UIController.instance.deactivateUI();
        preActiveNode.enabled = true;
        player.GetComponent<offlinePlayerMovement>().canMove = true;
        player.GetComponent<offlinePlayerShooting>().canFire = true;
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(2f);
        if (isSerenityWitch == true)
        {
            player.transform.position = NodeSpot.position;
            Camera.main.GetComponent<camFollowTarget>().resetPosition();
        }
        if(isSerenityWitch != true)
        {
            player.transform.position = SerenitySpot.position;
            Camera.main.GetComponent<camFollowTarget>().resetPosition();
        }
        yield return new WaitForSeconds(2f);
        FadeOut.instance.BeginFade(-1);

    }
    public void selectNo()
    {

        UIController.instance.deactivateUI();
        choiceUI.SetActive(false);
        player.GetComponent<offlinePlayerMovement>().canMove = true;
        player.GetComponent<offlinePlayerShooting>().canFire = true;
        preActiveNode.enabled = true;
    }
}
