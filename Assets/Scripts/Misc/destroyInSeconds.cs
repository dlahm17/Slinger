using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyInSeconds : MonoBehaviour {
    public float timeToDestroy;
    private void Start()
    {
        timeToDestroy = Time.time + timeToDestroy;
    }
    // Update is called once per frame
    void Update () {
		if(Time.time > timeToDestroy)
        {
            Destroy(gameObject);
        }
	}
}
