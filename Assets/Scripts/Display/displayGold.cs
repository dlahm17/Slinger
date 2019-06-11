using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayGold : MonoBehaviour
{
    Text myTxt;
    Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        myTxt = GetComponent<Text>();
        inv = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        myTxt.text = inv.checkGold().ToString();
    }
}
