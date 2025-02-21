using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum QuestTypeEnum
{
    Pickup,
    Deliver,
    Kill,
    Engage
}

[System.Serializable]
public struct questReward
{
    public int friendReward;

    public int ectoplasmReward;
}

[System.Serializable]
public struct basicQuest
{
    public string questName;

    public string questDetails;

    public QuestTypeEnum QuestType;

    public string pickupTag;

    public GameObject[] questObjectives;

    public bool isCompleted;

    public questReward questReward;

    public so_Dialogue CompletedQuestDialogue;
}

[CreateAssetMenu(fileName = "so_quest", menuName = "Quest", order = 1)]
public class QuestScriptableObj : ScriptableObject
{
    public basicQuest quest;
}