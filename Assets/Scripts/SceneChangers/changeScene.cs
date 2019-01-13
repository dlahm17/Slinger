using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class changeScene : MonoBehaviour {
    public string sceneToLoad;
    public void changeSceneImmediately()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
