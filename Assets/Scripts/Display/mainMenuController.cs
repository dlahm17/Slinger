using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Davis
/// purpose: to control the UI of the main menu.
/// </summary>
public class mainMenuController : MonoBehaviour
{
    public GameObject loadFile;
    public GameObject menu;
    public GameObject options;

    //All three of these functions are controlled by buttons in the main menu.  They control which panels are open and shut.
    public void openLoad()
    {
        menu.SetActive(false);
        options.SetActive(false);
        loadFile.SetActive(true);
    }
    public void openOptions()
    {
        menu.SetActive(false);
        loadFile.SetActive(false);
        options.SetActive(true);
    }
    public void returnToMain()
    {
        loadFile.SetActive(false);
        options.SetActive(false);
        menu.SetActive(true);
    }
}
