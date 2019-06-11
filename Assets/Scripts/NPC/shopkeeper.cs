using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopkeeper : NPCDialogueNode
{
    UIController UI;
    DialogueController DCtrl;

    public GameObject shopUI;
    public ShopUI sUI;

    int j = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        DCtrl = DialogueController.instance;
        UI = UIController.instance;

    }

    public override void activateDialogue()
    {
        if(myDialogue[j] == null)
        {
            Debug.Log("Dialogue null, resetting");
            DCtrl.DeactivateDialogue();
            j = 0;
            shopUI.SetActive(true);
            sUI.activate();
            active = false;
            return;
        }
        if(myDialogue[j] != null)
        {
            Debug.Log("Sending Dialogue upwards");
            DCtrl.ActivateDialogue(myDialogue[j], this);
            j++;
        }
    }
}
