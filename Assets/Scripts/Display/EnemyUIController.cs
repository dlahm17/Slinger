using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    #region singleton
    public static EnemyUIController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("TOO MANY UI CONTROLLERS");
            return;
        }
        instance = this;
    }
    #endregion
    public Text nameTxt;
    public Text healthTxt;
    public Slider hpSlider;

    public GameObject uiObject;
    float resetHPTime = 5f;
    float timeToReset = 0f;

    bool active = false;
    public void setUI(string name, float currenthealth, float maxhealth)
    {
        active = true;
        uiObject.SetActive(true);
        timeToReset = Time.time + resetHPTime;
        healthTxt.text = currenthealth.ToString() + "/" + maxhealth.ToString();
        hpSlider.maxValue = maxhealth;
        hpSlider.value = currenthealth;
        nameTxt.text = name;
    }
    public void deactivateUI()
    {
        active = false;
        uiObject.SetActive(false);
    }
    public void Update()
    {
        if(Time.time > timeToReset && active == true)
        {
            active = false;
            uiObject.SetActive(false);

        }
    }
}
