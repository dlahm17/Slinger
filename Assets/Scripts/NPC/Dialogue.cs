using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/DialogueTree")]
public class Dialogue : ScriptableObject
{
    new public string name = "New Dialogue";
    //speakerFace is the face of whoever is speaking in the dialogue
    public Sprite speakerFace = null;
    //the string is the voice line given with the dialogue
    public string dialogueString = "";
    
    
}
