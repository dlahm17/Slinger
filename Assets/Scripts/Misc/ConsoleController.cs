using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//This script handles all commands sent from command line
public class ConsoleController : MonoBehaviour
{
    public InputField myIn;
    private void OnEnable()
    {
        Debug.Log("enabled");
        EventSystem mySystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        mySystem.SetSelectedGameObject(myIn.gameObject);
    }
    public void onSelection()
    {
        //x is the current character we're on from the command
        int x = 0;
        //commandstring is the command itself
        string commandString = myIn.text;
        
        //checkchar will always be asking if the next character is a .
        char checkChar = commandString[x];

        //commandChar is the general command given
        char[] BaseCommandChar = new char[100];
        while (checkChar != '.' && x < commandString.Length)
        {
            //while the . is not found, it is reasonable to assume that the command will be there
            BaseCommandChar[x] = commandString[x];

            //we then make sure to check that the next letter is not a period, or the end of the string
            x++;
            if (x >= commandString.Length)
            {
                Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                myIn.text = "";
                devConsole.instance.onCommand();
                return;
            }
            checkChar = commandString[x];
        }
        //checkString is the finalized command after the command has ended, or a period found
      
        string checkString = "";
        int y = 0;
        while(y < x)
        {
            checkString += BaseCommandChar[y];
            y++;
        }
        

        
        //We then begin checking to make sure that the checkstring command is equal to valid commands
        if (checkString == "addgold")
        {
            int checker = 0;
            Debug.Log("addgold command detected");
            x++;
            checkChar = commandString[x];
            while(checkChar == '.')
            {
                if(checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }
            int amtToAdd;
            if(int.TryParse(subCheckString, out amtToAdd))
            {
                AddGold(amtToAdd);
            }
            else
            {
                Debug.LogWarning("Number not recognized");
            }
        }
        if (checkString == "subgold")
        {
            int checker = 0;
            Debug.Log("subgold command detected");
            x++;
            checkChar = commandString[x];
            while (checkChar == '.')
            {
                if (checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }
            int amtToAdd;
            if (int.TryParse(subCheckString, out amtToAdd))
            {
                AddGold(-amtToAdd);
            }
            else
            {
                Debug.LogWarning("Number not recognized");
            }
        }
        if (checkString == "addexp")
        {
            int checker = 0;
            Debug.Log("addexp command detected");
            x++;
            checkChar = commandString[x];
            while (checkChar == '.')
            {
                if (checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }
            int amtToAdd;
            if (int.TryParse(subCheckString, out amtToAdd))
            {
                AddExp(amtToAdd);
            }
            else
            {
                Debug.LogWarning("Number not recognized");
            }
        }
        if (checkString == "subexp")
        {
            int checker = 0;
            Debug.Log("addexp command detected");
            x++;
            checkChar = commandString[x];
            while (checkChar == '.')
            {
                if (checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }
            int amtToAdd;
            if (int.TryParse(subCheckString, out amtToAdd))
            {
                AddExp(-amtToAdd);
            }
            else
            {
                Debug.LogWarning("Number not recognized");
            }
        }
        if (checkString == "addwpn")
        {
            int checker = 0;
            Debug.Log("addwpn command detected");
            x++;
            checkChar = commandString[x];
            while (checkChar == '.')
            {
                if (checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }
            AddWeapon(subCheckString);
        }
        if (checkString == "addabi")
        {
            int checker = 0;
            Debug.Log("addabi command detected");
            x++;
            checkChar = commandString[x];
            while (checkChar == '.')
            {
                if (checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }
           AddAbility(subCheckString);
        }
        if (checkString == "spawn")
        {
            Debug.Log("Not including this for a while, spawning is... gross... at the moment");
        }
        if (checkString == "additem")
        {
            int checker = 0;
            Debug.Log("additem command detected");
            x++;
            checkChar = commandString[x];
            while (checkChar == '.')
            {
                if (checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }
            //we then subdivide to amulet
            if(subCheckString == "amulet")
            {
                int checker2 = 0;
                Debug.Log("Add Amulet Detected");
                x++;
                checkChar = commandString[x];
                while(checkChar == '.')
                {
                    if(checker2 > commandString.Length)
                    {
                        Debug.Log("command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandChar2 = new char[100];
                while(checkChar != '.' && x < commandString.Length)
                {
                    subCommandChar2[x] = commandString[x];
                    x++;
                    if(x >= commandString.Length)
                    {
                        Debug.Log("Command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckString2 = "";
                while(y < x)
                {
                    subCheckString2 += subCommandChar2[y];
                    y++;
                }
                AddAmulet(subCheckString2);
            }
            if (subCheckString == "boots")
            {
                int checker2 = 0;
                Debug.Log("Add Boots Detected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandChar2 = new char[100];
                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandChar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("Command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckString2 = "";
                while (y < x)
                {
                    subCheckString2 += subCommandChar2[y];
                    y++;
                }
                AddBoots(subCheckString2);
            }
            if (subCheckString == "armor")
            {
                int checker2 = 0;
                Debug.Log("Add Armor Detected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandChar2 = new char[100];
                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandChar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("Command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckString2 = "";
                while (y < x)
                {
                    subCheckString2 += subCommandChar2[y];
                    y++;
                }
                AddArmor(subCheckString2);
            }
            if (subCheckString == "hat")
            {
                int checker2 = 0;
                Debug.Log("Add hat Detected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandChar2 = new char[100];
                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandChar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("Command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckString2 = "";
                while (y < x)
                {
                    subCheckString2 += subCommandChar2[y];
                    y++;
                }
                AddHat(subCheckString2);
            }
            if (subCheckString == "consumable")
            {
                int checker2 = 0;
                Debug.Log("Add consumable Detected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandChar2 = new char[100];
                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandChar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("Command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckString2 = "";
                while (y < x)
                {
                    subCheckString2 += subCommandChar2[y];
                    y++;
                }
                AddConsumable(subCheckString2);
            }
        }
        if (checkString == "clearinventory")
        {
            ClearInventory();
        }
        if (checkString == "stat")
        {
            int checker = 0;
            Debug.Log("stat command detected");
            x++;
            checkChar = commandString[x];
            while (checkChar == '.')
            {
                if (checker > commandString.Length)
                {
                    Debug.Log("command unavailable, aborting");
                    return;
                }
                x++;
                checkChar = commandString[x];
                checker++;
            }
            y = x;
            //sub1Char is the same as commandchar from the initial investigation of commands
            char[] subCommandChar = new char[100];

            while (checkChar != '.' && x < commandString.Length)
            {
                subCommandChar[x] = commandString[x];
                x++;
                //if the char check goes off of the command string's length it'll end early and send a warning
                if (x >= commandString.Length)
                {
                    Debug.Log("Command not recognized, please check devConsoleReadmeFile for applicable commands");
                    return;
                }
                checkChar = commandString[x];
            }
            string subCheckString = "";
            while (y < x)
            {
                subCheckString += subCommandChar[y];
                y++;
            }

            if(subCheckString == "health")
            {
                int checker2 = 0;
                Debug.Log("health stat selected");
                x++;
                checkChar = commandString[x];
                while(checkChar == '.')
                {
                    if(checker2 > commandString.Length)
                    {
                        Debug.Log("Command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandchar2 = new char[100];

                while(checkChar != '.' && x < commandString.Length)
                {
                    subCommandchar2[x] = commandString[x];
                    x++;
                    if(x >= commandString.Length)
                    {
                        Debug.Log("command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckstring2 = "";
                while(y < x)
                {
                    subCheckstring2 += subCommandchar2[y];
                    y++;
                }
                int statToAdd;
                if(int.TryParse(subCheckstring2, out statToAdd))
                {
                    changeStat("health", statToAdd);
                }
            }
            if (subCheckString == "armor")
            {
                int checker2 = 0;
                Debug.Log("armor stat selected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("Command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandchar2 = new char[100];

                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandchar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckstring2 = "";
                while (y < x)
                {
                    subCheckstring2 += subCommandchar2[y];
                    y++;
                }
                int statToAdd;
                if (int.TryParse(subCheckstring2, out statToAdd))
                {
                    changeStat("armor", statToAdd);
                }
            }
            if (subCheckString == "damage")
            {
                int checker2 = 0;
                Debug.Log("damage stat selected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("Command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandchar2 = new char[100];

                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandchar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckstring2 = "";
                while (y < x)
                {
                    subCheckstring2 += subCommandchar2[y];
                    y++;
                }
                int statToAdd;
                if (int.TryParse(subCheckstring2, out statToAdd))
                {
                    changeStat("damage", statToAdd);
                }
            }
            if (subCheckString == "magicarmor")
            {
                int checker2 = 0;
                Debug.Log("magic armor stat selected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("Command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandchar2 = new char[100];

                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandchar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckstring2 = "";
                while (y < x)
                {
                    subCheckstring2 += subCommandchar2[y];
                    y++;
                }
                int statToAdd;
                if (int.TryParse(subCheckstring2, out statToAdd))
                {
                    changeStat("magicarmor", statToAdd);
                }
            }
            if (subCheckString == "speed")
            {
                int checker2 = 0;
                Debug.Log("speed stat selected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("Command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandchar2 = new char[100];

                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandchar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckstring2 = "";
                while (y < x)
                {
                    subCheckstring2 += subCommandchar2[y];
                    y++;
                }
                int statToAdd;
                if (int.TryParse(subCheckstring2, out statToAdd))
                {
                    changeStat("speed", statToAdd);
                }
            }
            if (subCheckString == "magicdamage")
            {
                int checker2 = 0;
                Debug.Log("magical damage stat selected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("Command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandchar2 = new char[100];

                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandchar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckstring2 = "";
                while (y < x)
                {
                    subCheckstring2 += subCommandchar2[y];
                    y++;
                }
                int statToAdd;
                if (int.TryParse(subCheckstring2, out statToAdd))
                {
                    changeStat("magicdamage", statToAdd);
                }
            }
            if (subCheckString == "stealth")
            {
                int checker2 = 0;
                Debug.Log("stealth stat selected");
                x++;
                checkChar = commandString[x];
                while (checkChar == '.')
                {
                    if (checker2 > commandString.Length)
                    {
                        Debug.Log("Command unavailable, aborting");
                        return;
                    }
                    x++;
                    checkChar = commandString[x];
                    checker2++;
                }
                y = x;
                char[] subCommandchar2 = new char[100];

                while (checkChar != '.' && x < commandString.Length)
                {
                    subCommandchar2[x] = commandString[x];
                    x++;
                    if (x >= commandString.Length)
                    {
                        Debug.Log("command not recognized, please check devconsolereadmefile for applicable commands");
                        return;
                    }
                    checkChar = commandString[x];
                }
                string subCheckstring2 = "";
                while (y < x)
                {
                    subCheckstring2 += subCommandchar2[y];
                    y++;
                }
                int statToAdd;
                if (int.TryParse(subCheckstring2, out statToAdd))
                {
                    changeStat("stealth", statToAdd);
                }
            }
        }
        myIn.text = "";
        devConsole.instance.onCommand();
    }

    void AddGold(int goldToAdd)
    {
        Inventory.instance.giveGold(goldToAdd);
    }
    void SubGold(int goldToSub)
    {
        Inventory.instance.giveGold(-goldToSub);
    }
    void AddExp(int expToAdd)
    {
        Inventory.instance.giveExp(expToAdd);
    }
    void SubExp(int expToSub)
    {
        Inventory.instance.giveExp(-expToSub);
    }
    void AddWeapon(string wpnName)
    {
        string pathString = "Misc/Items/Weapons/" + wpnName;
        Weapon wpn = (Weapon)Resources.Load(pathString, typeof(Weapon));
        bool added = Inventory.instance.Add(wpn);
        if (added)
        {
            Debug.Log("Weapon Added to Inventory");
        }
        else
        {
            Debug.Log("Failure to Add Weapon, check Name or weapon");
        }

    }
    void AddAbility(string abiName)
    {
        string pathString = "Misc/SpecialAbilities/" + abiName;
        SpecialAbilityObject myabi = (SpecialAbilityObject)Resources.Load(pathString, typeof(SpecialAbilityObject));
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<offlinePlayerShooting>().addSpAbility(myabi);
    }
    void AddAmulet(string amuletToAdd)
    {
        string pathString = "Misc/Items/Wearables/Amulets/" + amuletToAdd;
        Item amu = (Item)Resources.Load(pathString, typeof(Item));
        bool added = Inventory.instance.Add(amu);
        if (added)
        {
            Debug.Log("Amulet added to inventory");
        }
        else
        {
            Debug.Log("Failure to add amulet, check name or inventory space, one space must be available");
        }
    }
    void AddArmor(string armorToAdd)
    {
        string pathString = "Misc/Items/Wearables/Armor/" + armorToAdd;
        Item amu = (Item)Resources.Load(pathString, typeof(Item));
        bool added = Inventory.instance.Add(amu);
        if (added)
        {
            Debug.Log("Armor added to inventory");
        }
        else
        {
            Debug.Log("Failure to add armor, check name or inventory space, one space must be available");
        }
    }
    void AddBoots(string bootsToAdd)
    {
        string pathString = "Misc/Items/Wearables/Boots/" + bootsToAdd;
        Item amu = (Item)Resources.Load(pathString, typeof(Item));
        bool added = Inventory.instance.Add(amu);
        if (added)
        {
            Debug.Log("Boots added to inventory");
        }
        else
        {
            Debug.Log("Failure to add Boots, check name or inventory space, one space must be available");
        }
    }
    void AddHat(string hatToAdd)
    {
        string pathString = "Misc/Items/Wearables/Hats/" + hatToAdd;
        Item amu = (Item)Resources.Load(pathString, typeof(Item));
        bool added = Inventory.instance.Add(amu);
        if (added)
        {
            Debug.Log("Hat added to inventory");
        }
        else
        {
            Debug.Log("Failure to add Hat, check name or inventory space, one space must be available");
        }
    }
    void AddConsumable(string conToAdd)
    {
        string pathString = "Misc/Items/Consumables/" + conToAdd;
        Consumable con = (Consumable)Resources.Load(pathString, typeof(Consumable));
        bool added = Inventory.instance.Add(con);
        if (added)
        {
            Debug.Log("Consumable added to inventory");
        }
        else
        {
            Debug.Log("Failure to add consumable, check name or inventory space, one space must be available");
        }
    }
    void ClearInventory()
    {
        Inventory.instance.RemoveAll();
    }
    void changeStat(string statToChange, int statChangeAmt)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerStats pStat = player.GetComponent<playerStats>();
        if(statToChange == "health")
        {
            pStat.health.addModifier(statChangeAmt);
        }
        if(statToChange == "damage")
        {
            pStat.damage.addModifier(statChangeAmt);
        }
        if (statToChange == "armor")
        {
            pStat.armor.addModifier(statChangeAmt);
        }
        if (statToChange == "magicarmor")
        {
            pStat.magicArmor.addModifier(statChangeAmt);
        }
        if (statToChange == "speed")
        {
            pStat.speed.addModifier(statChangeAmt);
        }
        if (statToChange == "magicdamage")
        {
            pStat.magicDamage.addModifier(statChangeAmt);
        }
        if (statToChange == "stealth")
        {
            pStat.stealth.addModifier(statChangeAmt);
        }
    }





}
