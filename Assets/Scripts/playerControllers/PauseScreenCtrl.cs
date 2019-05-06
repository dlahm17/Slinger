using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenCtrl : MonoBehaviour {
    float bufferTime = .25f;
    float timetoReloadBuffer = .25f;

    bool pauseActive = false;
    public GameObject myScreen;
    GameObject player;
    offlinePlayerMovement mov;
    offlinePlayerShooting shot;
    
    // Update is called once per frame

    private void Start()
    {
        myScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        mov = player.GetComponent<offlinePlayerMovement>();
        shot = player.GetComponent<offlinePlayerShooting>();

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
            UIController.instance.deactivateUI();
            mov.canMove = true;
            shot.canFire = true;
            pauseActive = false;
            bufferTime = Time.time + timetoReloadBuffer;
            haveCalled = !haveCalled;
        }
        if (!pauseActive && !haveCalled)
        {
            UIController.instance.activateUI("PauseUI");
            mov.canMove = false;
            shot.canFire = false;
            pauseActive = true;
            bufferTime = Time.time + timetoReloadBuffer;
            haveCalled = !haveCalled;
        }
    }
}
