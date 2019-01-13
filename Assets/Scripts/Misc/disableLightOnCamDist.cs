using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableLightOnCamDist : MonoBehaviour {
    public Light myLight;
    GameObject[] player = new GameObject[4];
    public float maxDist;
    // Update is called once per frame


    private void OnEnable()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }
    void LateUpdate () {
        /*
        int i = 0;

        while (i < 4)
        {
            float distance = Vector3.Distance(player[i].transform.position, gameObject.transform.position);
            bool onscreen = Mathf.Abs(distance) < maxDist;
            if (onscreen == true)
            {
                Debug.Log("light on screen, turning on");
                myLight.enabled = true;
            }
            if (onscreen == false)
            {
                Debug.Log("light not on screen, turning off");
                myLight.enabled = false;
            }
            i++;
        }
        */
	}
    private void OnBecameInvisible()
    {
        myLight.enabled = false;
    }
    private void OnBecameVisible()
    {
        myLight.enabled = true;
    }
}
