using UnityEngine;
using UnityEngine.UI;

public class InteractablePickup : MonoBehaviour {

   //player layer is set in the editor, should be the layer labeled "player"
    public LayerMask PlayerLayer;
    //interaction radius is the distance the player has to be to interact with the object
    public float interactionRadius = 2f;
    //is in radius is the bool that determines if they're active.  It's public so the playermovement script can access it.
    public bool isInRadius = false;
    //This canvas houses the name of the object, the text that shows the name, and the name itself.
    Canvas myItemNameCanvas;
    Text myTxt;
    public string nameToShow;
    void OnDrawGizmosSelected()
    {
        //This gizmo will show the radius of the interaction
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
    public virtual void Start()
    {
        //This initializes the canvas and then disables it until the player is in range.
        if(myItemNameCanvas == null)
        {
            //myItemNameCanvas = GetComponent<Canvas>();
            if(myItemNameCanvas == null)
            {
                myItemNameCanvas = GetComponentInChildren<Canvas>();
            }
        }
        myTxt = myItemNameCanvas.GetComponentInChildren<Text>();
        myTxt.text = nameToShow;
        myItemNameCanvas.enabled = false;
    }
    private void Update()
    {
        if (myItemNameCanvas != null)
        {
            //update merely checks if the player can interact with it.
            isInRadius = Physics.CheckSphere(transform.position, interactionRadius, PlayerLayer);
        
            if (isInRadius)
            {
                myItemNameCanvas.enabled = true;
            }
            if (!isInRadius)
            {
                myItemNameCanvas.enabled = false;
            }
        }
    }

    public virtual void Interact()
    {
        //interact will be changed per the interactable pickups
        Debug.Log("Interacting with" + gameObject.name);
    }
}
