using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLineTwoPoints : MonoBehaviour
{
    GameObject origin;
    public GameObject[] target = new GameObject[10];

    private void OnDrawGizmos()
    {
        if(origin == null)
        {
            origin = gameObject;
        }
        foreach (GameObject target in target) {
            if (target != null)
            {
                Gizmos.color = Color.red;
               
                Gizmos.DrawLine(origin.transform.position, target.transform.position);
            }
        }
        
    }

}
