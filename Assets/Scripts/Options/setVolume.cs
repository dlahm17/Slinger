using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setVolume : MonoBehaviour
{
    public AudioListener mylistener;
    public void LateUpdate()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("MasterVolume");
    }
}
