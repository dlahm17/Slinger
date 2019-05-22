using System;
using System.Collections;
[Serializable]
public class GameData 
{
    //Here we will have the data that the game will save and load and translate

    //The save file is the number of the save file this data is saved to.  1 - 12.
    public int saveFile;
    //An array of 20 items will hold all the items that the player is holding onto.
    public InventoryData[] inventoryItems = new InventoryData[20];
    //Player Equipment is held in an array
    public EquipmentData[] Player_Equipment = new EquipmentData[5];
    //Boss Data is stored by using a name and a bool to show if it's beaten
    public BossData[] Bossdata = new BossData[10];
    //Area Data is stored by using a name and a bool to show if it's been passed
    public AreaData[] areaData = new AreaData[20];
    //Node data is stored by using a bool to determine if it's bought, and a stat modifier to determine what stat it is.
    public NodeData[] node_Bought = new NodeData[300];
    //event data is stored by using a nameID and a bool to show if the event has happened;
    public EventData[] eventData = new EventData[8];
    //Current Time of game will be placed in a double
    public double time;
    //Interactable pickups will be stored with a name ID and a bool to show if it has been picked up in the world.
    public InteractablePickupData[] pickupData = new InteractablePickupData[500];





}
