using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTransform : MonoBehaviour
{
    public GameObject target;
    void Update()
    {
        gameObject.transform.rotation = target.transform.rotation;
        gameObject.transform.position = target.transform.position;
    }
}
