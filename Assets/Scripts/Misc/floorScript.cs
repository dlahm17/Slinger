using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorScript : MonoBehaviour {
    public Material invisMaterial;
    public Material regMaterial;

    Renderer myrender;
    GameObject player;

    public bool isInvis = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myrender = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("InvisTrigger") && player.transform.position.y < gameObject.transform.position.y)
        {
            myrender.material = invisMaterial;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("InvisTrigger"))
        {
            myrender.material = regMaterial;
        }
    }

}
