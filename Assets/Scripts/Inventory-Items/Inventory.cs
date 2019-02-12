using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    //There's only one inventory in any scene
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
    //Allow other scripts to add their own methods to whenever an item is changed (for example, equipment manager, stats, etc.)
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //space is how many items that the player to hold.
    public int space = 20;

    public List<Item> items = new List<Item>();

    //Current gold and experience handles the gold and experience the the player currently owns.s
    [SerializeField]
    float currentGold;
    [SerializeField]
    float currentExp;

    //These fields are for showing the UI, the gold, and the experience.
    [SerializeField]
    GameObject UIObj;
    [SerializeField]
    Text goldTxt;
    [SerializeField]
    Text expTxt;

    public List<Key> keys = new List<Key>();

    public void giveGold(int goldtoGive)
    {
        currentGold += goldtoGive;
    }
    private void Update()
    {

        if (goldTxt != null && expTxt != null)
        {
            goldTxt.text = "G: " + currentGold.ToString();
            expTxt.text = "EXP: " + currentExp.ToString();
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

    public void addKey(Key keyToAdd)
    {
        if (keys.Contains(keyToAdd)){
            Debug.LogError("Key already obtained, fuck off");
        }
        else
        {
            keys.Add(keyToAdd);
        }
    }

    public bool queryKey(Key checkingKey)
    {
        if (keys.Contains(checkingKey))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
