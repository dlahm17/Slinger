using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour {
    public Material invisMaterial;
    private Material regMaterial;

    MeshRenderer myrender;
   
    public bool isInvis = false;
    /*
    private void Start()
    {
        myrender = GetComponent<MeshRenderer>();
        regMaterial = myrender.materials[0];
    }
    private void Update()
    {
        if (isInvis)
        {
            if(myrender.material != invisMaterial)
            {
                myrender.materials[0] = invisMaterial;
            }
        }
        if (!isInvis)
        {
            if(myrender.material != regMaterial)
            {
                myrender.materials[0] = regMaterial;
            }
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Collision");
        if(other.tag.Equals("InvisTrigger"))
        {
            Debug.Log("We going invisible bois");
            isInvis = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("InvisTrigger"))
        {
            Debug.Log("Back to visible");
            isInvis = false;
        }
    }

}
