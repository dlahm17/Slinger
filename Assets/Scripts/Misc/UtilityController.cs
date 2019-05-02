using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityController : MonoBehaviour
{
    #region singleton
    public static UtilityController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("multiple versions of the Utility Controller in scene");
            return;
        }
        instance = this;
    }
    #endregion
    public void teleport(GameObject g, int x, int y, int z)
    {
        g.transform.position = new Vector3(x, y, z);

    }
    public void fadeOut(float timeToFadeOut)
    {
    }
}

