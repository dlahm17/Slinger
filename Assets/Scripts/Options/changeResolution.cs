using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class changeResolution : MonoBehaviour
{
    public Dropdown myDrop;
    public Toggle fullscreen;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Res") == 0)
        {
            myDrop.value = 0;
        }
        if(PlayerPrefs.GetInt("Res") == 1)
        {
            myDrop.value = 1;
        }
        if(PlayerPrefs.GetInt("Res") == 2)
        {
            myDrop.value = 2;
        }
        if(Screen.fullScreen == true)
        {
            fullscreen.isOn = true;
        }
        if(Screen.fullScreen == false)
        {
            fullscreen.isOn = false;
        }
    }

    public void changeGameResolution()
    {
        bool full = false;
        if(fullscreen.isOn == true)
        {
            full = true;
        }
        if(myDrop.value == 0)
        {
            Screen.SetResolution(1920, 1080, full);
            PlayerPrefs.SetInt("Res", 0);
        }
        if(myDrop.value == 1)
        {
            Screen.SetResolution(1600, 900, full);
            PlayerPrefs.SetInt("Res", 1);
        }
        if(myDrop.value == 2)
        {
            Screen.SetResolution(1280, 720, full);
            PlayerPrefs.SetInt("Res", 2);
        }
    }
}
