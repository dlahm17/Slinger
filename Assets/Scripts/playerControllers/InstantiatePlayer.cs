using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class InstantiatePlayer : MonoBehaviour {
    public GameObject GM;
    private void Start()
    {
        GameObject n;
        n = Instantiate(GM);
        n.name = "GameManager";
        
    }
}
