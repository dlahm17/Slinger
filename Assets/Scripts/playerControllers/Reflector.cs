using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    GameObject player;
    Camera myCam;
    float camRayLength = 100f;
    LayerMask floormask;
    public Transform looker;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myCam = Camera.main;
        floormask = LayerMask.NameToLayer("Floor");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Projectile"))
        {
            Debug.Log("Reflecting");
            other.gameObject.transform.LookAt(looker);
            other.gameObject.GetComponent<bulletForce>().origin = this.gameObject;
            other.gameObject.transform.rotation = new Quaternion(0, other.gameObject.transform.rotation.y, other.gameObject.transform.rotation.z, 1);
        }
    }
}
