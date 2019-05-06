using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    #region singleton
    public static FadeOut instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("fadeout already in scene");
            return;
        }
        instance = this;
    }
    #endregion
    public Texture2D fadeOutTexture;
    public float fadeSpeed = .8f;
    public int drawDepth = -1000;
    private float alpha = 1.0f;

    private int fadeDir = -1;
    private void Start()
    {
        SceneManager.sceneLoaded += levelLoaded;
    }
    

    void OnGUI()
    {
        //fade out/in the alpha value using a direction, a speed and time.deltatime to convert the operation to seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        //force clamp the number between 0 and 1 because gui.color uses alpha values between 0 and 1;
        alpha = Mathf.Clamp01(alpha);
        //set color of our gui in this case our texture
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);


        GUI.depth = drawDepth;  //make the black texture render on top of everything;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);  //draw the texture to fit entire screen area

    }
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);  //return it so its easy to time the app.loadlevel
    }

    private void levelLoaded(Scene scene, LoadSceneMode mode)
    {
        BeginFade(-1);
    }
}
