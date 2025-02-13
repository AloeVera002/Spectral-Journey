using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct basicQuest
{
    public string questName;

    public string questId;

    public string questDetails;

    public string pickupTag;

    public GameObject[] questObjective;

    public bool isKilling;

    public bool isCompleted;

    public int questProgress;

    public int friendshipIncreaseValue;

    public int ectoplasmReward;

    public so_Dialogue CompletedQuestDialogue;
}

[System.Serializable]
public struct QuestStruct
{
    public basicQuest quest;

    public UnityEvent[] reward;

    public void InvokeFunction(int index)
    {
        if (index >= 0 && index < reward.Length)
        {
            reward[index]?.Invoke();
        }
    }
}

[CreateAssetMenu(fileName = "QuestScriptableObj", menuName = "Quest", order = 1)]
public class QuestScriptableObj : ScriptableObject
{
    public basicQuest quest;
}