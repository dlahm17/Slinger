using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisling_LevelUpNode : NPCDialogueNode
{
    //The node will send dialogue items to the controller.  The node can hold up to 100 items.
    
    DialogueController dialogueCtrll;
    int j = 0;
    public Choice_TP_Nodes choice;
    public override void Start()
    {
        //This gets the variables for the dialogue controller and player along with setting the node properly.  Note that the NPC behaviour should control what node is active on the npc's dialogue tree.
        base.Start();
        dialogueCtrll = DialogueController.instance;
        j = 0;
    }
    public override void activateDialogue()
    {
        if (myDialogue[j] == null)
        {
            Debug.Log("Dialogue null, resetting");
            dialogueCtrll.DeactivateDialogue();
            j = 0;
            choice.enabled = true;
            choice.activate();
            active = false;
            return;
        }
        if (myDialogue[j] != null)
        {
            Debug.Log("Sending Dialogue Upwards");
            dialogueCtrll.ActivateDialogue(myDialogue[j], this);
            j++;
        }
    }
}
