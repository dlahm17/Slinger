using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    #region singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;


        
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public float currentGold;
    public float currentExp;

    [SerializeField]
    GameObject UIObj;
    [SerializeField]
    Text goldTxt;
    [SerializeField]
    Text expTxt;

    public void giveGold(int goldtoGive)
    {
        currentGold += goldtoGive;
    }
    private void Update()
    {

        if (goldTxt != null && expTxt != null)
        {
            goldTxt.text = currentGold.ToString();
            expTxt.text = currentExp.ToString();
        }
    }
    public bool Add(Item item)
    {
        
            if(items.Count >= space)
            {
                Debug.Log("Not Enough Room");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
