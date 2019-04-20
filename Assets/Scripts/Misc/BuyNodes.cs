using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyNodes : MonoBehaviour
{
    Button myButton;
    drawLineTwoPoints myCtrl;
    void Start()
    {
        myCtrl = GetComponent<drawLineTwoPoints>();
        myButton = GetComponent<Button>();
        if (myCtrl.active)
        {
            myButton.interactable = true;

        }
        else
        {
            myButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
