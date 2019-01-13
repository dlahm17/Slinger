using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferTransformControls : MonoBehaviour {
    public void setPosition(float Xchange,float YChange,  float Zchange)
    {
        transform.position = new Vector3(transform.position.x + Xchange, YChange, transform.position.z + Zchange);
        return;
    }
}
