using UnityEngine;
using System.Collections;

[System.Serializable]
public class Dialogue
{
    public string npcName;

    [TextArea(3,10)]
    public string[] sentences;
}
