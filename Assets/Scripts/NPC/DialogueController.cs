using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    
    #region singleton

    public static DialogueController instance;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion
    //DialogueGameObject is the GO that contains the visuals for the UI
    public GameObject DialogueGameObject;
    NPCDialogueNode currentlyActiveNode;


    //SpeakerText and SpeakerImage are the faceshot of the speaker and the text they're speaking
    public Text speakerText;
    public Image speakerImage;
    //Dialogueactive is the bool that when set to true it'll activate the gameobject to show the Text.  It'll be deactivated when the text string is done
    public bool dialogueActive = false;
    
    //activatedialogue is called from the npc dialogue node when the player interacts with them.
    public void ActivateDialogue(Dialogue dialogueToSpeak, NPCDialogueNode activeNode)
    {
        currentlyActiveNode = activeNode;
        dialogueActive = true;
        speakerText.text = dialogueToSpeak.dialogueString;
        speakerImage.sprite = dialogueToSpeak.speakerFace;
    }

    //Deactivate Dialogue is called from the NPC dialogue node once the dialogue is finished
    public void DeactivateDialogue()
    {
        speakerText.text = "";
        speakerImage.sprite = null;
        dialogueActive = false;
        currentlyActiveNode = null;
        
    }

    private void Update()
    {
        //This asks if the dialoguegameobject's activity is equal to the active state of the controller and changes it do be so.
        if(!dialogueActive && DialogueGameObject.activeInHierarchy)
        {
            DialogueGameObject.SetActive(false);
        }
        if(dialogueActive && !DialogueGameObject.activeInHierarchy)
        {
            DialogueGameObject.SetActive(true);
        }
        if(dialogueActive && DialogueGameObject.activeInHierarchy && Input.GetButtonDown("Interact"))
        {
            if(currentlyActiveNode != null)
            {
                currentlyActiveNode.activateDialogue();
            }
        }
    }
}
