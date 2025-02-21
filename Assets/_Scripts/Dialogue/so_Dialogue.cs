using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct dialogueLine
{
    public string Name;

    [TextArea(1, 3)]
    public string text;

    public bool isQuestion;

    public bool isGiveQuest;

    public bool isReward;

    public so_Dialogue nextDialogue;
}

[CreateAssetMenu(fileName = "so_Dialogue", menuName = "Dialogue")]
public class so_Dialogue : ScriptableObject
{
    public dialogueLine[] dialogue;
}