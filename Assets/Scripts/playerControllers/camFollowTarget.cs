using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollowTarget : MonoBehaviour {
    public Transform target;
    public Vector3 offset;
    Vector3 baseOffset;
    public float rayLength;

    GameObject hitWall;
    wallScript myWall;
    

    bool hasTarget;

    EnemyGridController currentGrid;

    float maxRangeX = 1000000000;
    float minRangeX = -1000000000;
    float maxRangeZ = 1000000000;
    float minRangeZ = -1000000000;
    float movementSpeed = 15f;
    float normalizedspeed;

    bool clamped = true;

    private void Start()
    {
        Vector3 targetPosition = target.position - offset;
        transform.position = targetPosition;
        baseOffset = offset;
    }


    public EnemyGridController getCurrentGrid()
    {
        return currentGrid;
    }
    
    public void resetPosition()
    {
        Debug.Log("Resetting Camera Position");
        transform.position = target.position - offset;
    }

    public void getNewGrid(EnemyGridController newGrid, Vector3 offsetChange)
    {
        currentGrid = newGrid;
        if(offsetChange != new Vector3(0, 0, 0))
        {
            offset = offsetChange;
        }
        if(offsetChange == new Vector3(0, 0, 0))
        {
            offset = baseOffset;
        }

        //float RadX = newGrid.radiusX;
        //float RadZ = newGrid.radiusZ;

        maxRangeX = newGrid.bufX.position.x;
        minRangeX = newGrid.minBufX.position.x;
        maxRangeZ = newGrid.bufZ.position.z;
        minRangeZ = newGrid.minBufZ.position.z;
        clamped = false;
        
        return;
    }

    // Update is called once per frame
    void Update () {
        if (target == null)
        {
            return;
        }
        if (target != null)
        {
            Vector3 targetPosition = target.position - offset;
            normalizedspeed = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, normalizedspeed);
            if (clamped)
            {
                float xPos = Mathf.Clamp(transform.position.x, minRangeX, maxRangeX);
                float ZPos = Mathf.Clamp(transform.position.z, minRangeZ, maxRangeZ);
                transform.position = new Vector3(xPos, target.position.y - offset.y, ZPos);
            }
            if (!clamped)
            {
                if(transform.position.x < minRangeX)
                {
                    transform.Translate(Vector3.right * Time.deltaTime);
                    
                }
                if(transform.position.x > maxRangeX)
                {
                    transform.Translate(Vector3.left * Time.deltaTime);
                }
                if(transform.position.z < minRangeZ)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime);
                }
                if(transform.position.z > maxRangeZ)
                {
                    transform.Translate(Vector3.back * Time.deltaTime);
                }

                if(transform.position.x< maxRangeX && transform.position.x > minRangeX && transform.position.z > minRangeZ && transform.position.z < maxRangeZ)
                {
                    clamped = true;
                }
               
            }



        }
	}

    public bool returnTarget()
    {
        if(target == null)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}
