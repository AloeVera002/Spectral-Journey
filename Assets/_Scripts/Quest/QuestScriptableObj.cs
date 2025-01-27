using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct basicQuest
{
    public string questName;

    public string questId;

    public string questDetails;

    public bool isFetchQuest;

    public bool isCollectableReward;

    public GameObject rewardObj;
}

[System.Serializable]
public struct rewardQuest
{
    public bool isCollectableReward;

    public GameObject rewardObj;
}

[System.Serializable]
public struct QuestStruct
{
    public basicQuest quest;

    public rewardQuest[] rewardsToDec;

    public UnityEvent[] reward;

    public void InvokeFunction(int index)
    {
        if (index >= 0 && index < reward.Length)
        {
            reward[index]?.Invoke();
        }
    }
}

[CreateAssetMenu(fileName = "QuestScriptableObj", menuName = "Scriptable Objects/Quest", order = 1)]
public class QuestScriptableObj : ScriptableObject
{
    public QuestStruct[] quests;
}