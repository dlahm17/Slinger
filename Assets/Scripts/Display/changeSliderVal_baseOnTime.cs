using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Davis L
/// Purpose: To increase a slider's value based on how long it has been.  This is most useful for sliders like the magic effects sliders.
/// </summary>
public class changeSliderVal_baseOnTime : MonoBehaviour
{
    Slider me;
    private void Start()
    {
        //Grab the slider named Me on startup
        me = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        //for every frame we update the value of the slider to the current time.  This is only most effective when the max value of the slider is based on Time.time + (insert value).  This would not be useful for HP values, but timing is fine.
        me.value = Time.time;
    }
}
