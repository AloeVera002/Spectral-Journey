using System.Collections.Generic;
using UnityEngine;

public class TestQuestManagerScript : MonoBehaviour
{
    private Dictionary<int, TestQuestScript> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private Dictionary<int, TestQuestScript> CreateQuestMap()
    {
        so_Quest[] allQuest = Resources.LoadAll<so_Quest>("Quests");

        Dictionary<int, TestQuestScript> idToQuestMap = new Dictionary<int, TestQuestScript>();
        foreach (so_Quest soQuest in allQuest)
        {
            if (idToQuestMap.ContainsKey(soQuest.questID))
            {
                Debug.Log("duplicate found when creating QuestMap");
            }
            idToQuestMap.Add(soQuest.questID, new TestQuestScript(soQuest));
        }
        return idToQuestMap;
    }

    private TestQuestScript GetQuestById(int id)
    {
        TestQuestScript questWithId = questMap[id];
        if (questWithId != null)
        {
            Debug.Log("quest with ID returned null");
        }
        return questWithId;
    }
}
