using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
    public LineRenderer connector;
    public GameObject connector1;
    public GameObject connector2;

    public void addConnector(drawLineTwoPoints d)
    {
        if(connector1 != d && connector2 != d)
        {
            if(connector1 != null)
            {
                connector1 = d.gameObject;
                drawline();
                return;
            }
            if(connector2 != null)
            {
                connector2 = d.gameObject;
                drawline();
                return;
            }
        }
    }
    void drawline()
    {
        if(connector1 != null && connector2 != null)
        {
            connector.SetPosition(0, connector1.transform.position);
            connector.SetPosition(1, connector2.transform.position);
        }
    }
    public void changeMaterial(Material m)
    {
        connector.material = m;
    }
}
