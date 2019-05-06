using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyInSeconds : MonoBehaviour
{
    public float timeTilDestruction = 2f;
    float timetoDestroy;
    private void Start()
    {
        setTime();
    }
    // Start is called before the first frame update
    public void setTime()
    {
        timetoDestroy = Time.time + timeTilDestruction;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timetoDestroy)
        {
            Destroy(gameObject);
        }
    }
}
