using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Door", menuName = "SceneChanger/Door")]
public class Door : ScriptableObject
{

    /// <summary>
    /// Door is the individual doors in a world, they all have a unique ID number that correlates to the same door in a different area (which is also internalized here)
    /// </summary>
    public int myID;
    public string sceneToChangeTo = "";

}
