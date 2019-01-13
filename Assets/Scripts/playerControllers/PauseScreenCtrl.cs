using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenCtrl : MonoBehaviour {
    float bufferTime = .25f;
    float timetoReloadBuffer = .25f;

    bool pauseActive = false;
    public GameObject myScreen;
    
    // Update is called once per frame

    private void Start()
    {
        myScreen.SetActive(false);
    }
    void Update () {
        if (Input.GetButtonDown("Pause") && Time.time > bufferTime)
        {
            callPause();
        }


	}

    public void callPause()
    {
        bool haveCalled = false;
        if (pauseActive && !haveCalled)
        {
            myScreen.SetActive(false);
            pauseActive = false;
            bufferTime = Time.time + timetoReloadBuffer;
            haveCalled = !haveCalled;
        }
        if (!pauseActive && !haveCalled)
        {
            myScreen.SetActive(true);
            pauseActive = true;
            bufferTime = Time.time + timetoReloadBuffer;
            haveCalled = !haveCalled;
        }
    }
}
