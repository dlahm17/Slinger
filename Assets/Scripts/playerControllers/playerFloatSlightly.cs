using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFloatSlightly : MonoBehaviour {
    public float floatDist;
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up * floatDist, out hit))
        {
            if(hit.collider.tag.Equals("Floor") && hit.distance < floatDist)
            {
                gameObject.transform.position = new Vector3(transform.position.x, hit.transform.position.y + floatDist, transform.position.z);
            }
            
        }
        
	}
}
