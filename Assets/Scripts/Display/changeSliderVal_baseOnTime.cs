using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeSliderVal_baseOnTime : MonoBehaviour
{
    Slider me;
    private void Start()
    {
        me = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        me.value = Time.time;
    }
}
