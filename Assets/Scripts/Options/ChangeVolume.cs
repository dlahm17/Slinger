using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    /// <summary>
    /// change value changes the value of the playerpref in regards to the master volume.
    /// </summary>
    // Start is called before the first frame update
    public Slider mySlider;
    // Update is called once per frame
    private void Start()
    {
        mySlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }
    void Update()
    {
        PlayerPrefs.SetFloat("MasterVolume", mySlider.value);
    }
}
