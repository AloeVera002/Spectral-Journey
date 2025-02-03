using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct dialogueLine
{
    public string Name;

    [TextArea(1, 3)]
    public string text;

    public bool isQuestion;

    public bool giveQuest;

    public so_Dialogue AcceptedDialogue;
    public so_Dialogue RejectedDialogue;
}

[CreateAssetMenu(fileName = "so_Dialogue", menuName = "Dialogue")]
public class so_Dialogue : ScriptableObject
{
    public dialogueLine[] dialogue;
}