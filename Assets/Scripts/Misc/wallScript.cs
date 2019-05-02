using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour {

    MeshRenderer myrender;
   
    public bool isInvis = false;


    private void Start()
    {
        myrender = GetComponent<MeshRenderer>();
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Collision");
        if(other.gameObject.tag.Equals("InvisTrigger"))
        {
            Debug.Log("We going invisible bois");
            Color color = myrender.material.color;
            color.a = .5f;
            myrender.material.color = color;
            isInvis = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("InvisTrigger"))
        {
            Debug.Log("Back to visible");
            Color color = myrender.material.color;
            color.a = 1f;
            myrender.material.color = color;
            isInvis = false;
        }
    }
    */

}
