using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InstantiatePlayer : MonoBehaviour {

    GameObject networkManager;
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            networkManager = GameObject.Find("NetworkManager");
            networkManager.GetComponent<NetworkManager>().StartHost();
            Destroy(gameObject);
        }
	}
}
