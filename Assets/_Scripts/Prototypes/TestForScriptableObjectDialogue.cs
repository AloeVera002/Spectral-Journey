using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct dialogueLines
{
    public string Name;

    [TextArea(1, 3)]
    public string text;
}

[CreateAssetMenu(fileName = "SO Dialogue", menuName = "Scriptable Objects/DialogueTest")]
public class TestForScriptableObjectDialogue : ScriptableObject
{
    public dialogueLines[] dialogue;
}
