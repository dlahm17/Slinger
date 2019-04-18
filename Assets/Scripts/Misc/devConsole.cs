using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devConsole : MonoBehaviour
{
    //This script just controls if the dev console is active or not.
    //When it's active, dev commands can be delivered to the console
    #region singleton
    public static devConsole instance;

    private void Awake()
    {
        PlayerPrefs.SetFloat("devConsoleUp", 0);
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of developer console found!");
            return;
        }
        instance = this;


    }
    #endregion

    public GameObject console;
    bool active = false;
    // Update is called once per frame
    public void onCommand()
    {
            bool action = false;
            if(!active && !action)
            {
                
                console.SetActive(true);
            PlayerPrefs.SetFloat("devConsoleUp", 1);
            active = true;
                action = true;
            }
            if(active && !action)
            {
                console.SetActive(false);
            PlayerPrefs.SetFloat("devConsoleUp", 0);
            active = false;
                action = true;
            }
    }   
    
}
