using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image itemImg;
    public ShopUI myShop;
    public Item ItemToAdd;
    public int cost = 10;
    public Text CostTxt;
    // Start is called before the first frame update
    void Start()
    {
        CostTxt.text = cost.ToString();
        itemImg.sprite = ItemToAdd.Icon;
    }

    // Update is called once per frame
    public void buy()
    {
        myShop.buy(ItemToAdd, cost);
    }
}
