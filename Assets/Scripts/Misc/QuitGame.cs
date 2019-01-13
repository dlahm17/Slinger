using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class QuitGame : MonoBehaviour
{
    public void quitGame()
    {
        if (SteamManager.Initialized)
        {
            SteamAPI.Shutdown();
        }
        Application.Quit();
    }
}
