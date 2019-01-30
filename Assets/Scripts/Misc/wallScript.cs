using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour {
    public Material invisMaterial;
    private Material regMaterial;

    Renderer myrender;
   
    public bool isInvis = false;

    private void Start()
    {
        myrender = GetComponent<Renderer>();
        regMaterial = myrender.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("InvisTrigger"))
        {
            Debug.Log("We going invisible bois");
            myrender.material = invisMaterial;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("InvisTrigger"))
        {
            Debug.Log("Back to visible");
            myrender.material = regMaterial;
        }
    }

}
