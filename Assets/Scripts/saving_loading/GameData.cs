using System;
using System.Collections;
[Serializable]
public class GameData 
{
    //Here we will have the data that the game will save and load and translate

    //The save file is the number of the save file this data is saved to.  1 - 12.
    public int saveFile;
    //An array of 20 items will hold all the items that the player is holding onto.
    public Item[] inventoryItems = new Item[20];
    //Player Equipment is held in an array
    public Equipment[] Player_Equipment = new Equipment[5];
    //Boss Data is stored by using a name and a bool to show if it's beaten
    public BossData[] Bossdata = new BossData[10];
    //Area Data is stored by using a name and a bool to show if it's been passed
    public AreaData[] areaData = new AreaData[20];
    //Node data is stored by using a bool.  Each node will have its own ID number, and that number will correlate with the bool in this array
    public bool[] node_Bought = new bool[300];
    //event data is stored by using a nameID and a bool to show if the event has happened;
    public EventData[] eventData = new EventData[8];
    //Current Time of game will be placed in a double
    public double time;
    //Interactable pickups will be stored with a name ID and a bool to show if it has been picked up in the world.
    public InteractablePickupData[] pickupData = new InteractablePickupData[500];





}
