using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSaveFile : MonoBehaviour
{
    public DataSave_Load myLoader;
    public void saveFile1()
    {
        myLoader.savefile = 1;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile2()
    {

        myLoader.savefile = 2;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile3()
    {
        myLoader.savefile = 3;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile4()
    {
        myLoader.savefile = 4;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile5()
    {
        myLoader.savefile = 5;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile6()
    {
        myLoader.savefile = 6;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile7()
    {
        myLoader.savefile = 7;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile8()
    {
        myLoader.savefile = 8;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile9()
    {
        myLoader.savefile = 9;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
    public void saveFile10()
    {
        myLoader.savefile = 10;
        myLoader.updateSaveFile();
        myLoader.loadGame();
        FadeOut.instance.BeginFade(1);
        SceneManager.LoadSceneAsync("Jaggenworth_Serenity");
    }
}
