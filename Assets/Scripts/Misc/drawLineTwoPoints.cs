using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class drawLineTwoPoints : MonoBehaviour
{
    //This script will merely set the node script's values, and activate lines/materials when the node has been bought.
    GameObject origin;
    node myNode;
    public GameObject[] target = new GameObject[10];
    public Material ActiveMaterial;
    public Material InActiveMaterial;
    public bool active = false;
    public bool haveBought = false;
    int baseCost = 5;
    int myCost;
    int tier = 1;
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
    
    private void Awake()
    {
        myNode = GetComponent<node>();
        
        if(gameObject.name == "NodeT1")
        {
            myCost = baseCost * 1;
        }
        if (gameObject.name == "NodeT2")
        {
            myCost = baseCost * 2;
        }
        if (gameObject.name == "NodeT3")
        {
            myCost = baseCost * 5;
        }
        if (gameObject.name == "NodeT4")
        {
            myCost = baseCost * 10;
        }
        if (gameObject.name == "LargeNodeT1")
        {
            myCost = baseCost * 10;
        }
        if (gameObject.name == "LargeNodeT2")
        {
            myCost = baseCost * 20;
        }
        if (gameObject.name == "LargeNodeT3")
        {
            myCost = baseCost * 30;
        }
        if (gameObject.name == "LargeNodeT4")
        {
            myCost = baseCost * 40;
        }
        if (gameObject.name == "LargeNodeT1")
        {
            myCost = baseCost * 50;
        }
        
        myNode.cost = myCost;
        myNode.isAvailableForPurchase = active;
        myNode.isPurchased = haveBought;

    }
    public void setActive()
    {
        active = true;
        foreach (GameObject go in target)
        {
            if (go != null)
            {
                node n = go.GetComponent<node>();
                if (n != null)
                {
                    n.isAvailableForPurchase = true;
                }
            }
        }
    }

    public GameObject[] ReturnAllConnections()
    {
        int x = 0;
        GameObject[] connections = new GameObject[target.Length];
        foreach(GameObject go in target)
        {
            if(go != null)
            {
                connections[x] = go;
                x++;
            }
        }
        return connections;
    }



}
