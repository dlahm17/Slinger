using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Control : MonoBehaviour
{
    bool isLower = false;
    bool isMoving = false;

    public float maxSpeed = 5f;
    float currentSpeed = 0;
    [Space]
    public float acceleration = .25f;

    public Transform UpperPoint;
    public Transform LowerPoint;
    
    


    public bool queryLower()
    {
        if (isLower)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (isLower)
            {
                if(transform.position.y < UpperPoint.position.y)
                {
                    Debug.Log("Going up");
                    Debug.Log(currentSpeed);
                    currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration);
                    transform.Translate(new Vector3(0, currentSpeed * Time.deltaTime, 0));
                    if(transform.position.y >= UpperPoint.position.y)
                    {
                        isMoving = false;
                        currentSpeed = 0;
                    }
                }
            }
            if (!isLower)
            {
                if(transform.position.y > LowerPoint.position.y)
                {
                    Debug.Log("Going down");
                    Debug.Log(currentSpeed);
                    currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration);
                    transform.Translate(new Vector3(0, -currentSpeed * Time.deltaTime, 0));
                    if (transform.position.y <= LowerPoint.position.y)
                    {
                        isMoving = false;
                        currentSpeed = 0;
                    }
                }
            }
        }
    }

    public void activate()
    {
        Debug.Log("Elevator Active");
        isLower = !isLower;
        isMoving = true;
    }
}
