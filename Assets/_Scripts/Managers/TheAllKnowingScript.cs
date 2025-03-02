using UnityEngine;

public class TheAllKnowingScript : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject[] questGivers;
    [SerializeField] int questIndex = 0;
    private QuestGiver quester;

    void Start()
    {
        ActivateQuestGiver();
    }

    void Update()
    {
    }

    public void UpdateQuestGiver()
    {
        Debug.Log("Called UpdateQuest");
        ResetQuestGiver();
        questIndex += 1;
        ActivateQuestGiver();
    }

    void ActivateQuestGiver()
    {
        quester = questGivers[questIndex].GetComponent<QuestGiver>();
        quester.hasQuestToGive = true;
        quester.hasDialogue = true;
        quester.isQuestGiver = true;
        quester.ToggleNPCMark(0);
        Debug.Log("quest giver: " + quester.gameObject.name);
    }

    void ResetQuestGiver()
    {
        if (quester)
        {
            quester.hasQuestToGive = false;
            quester.hasDialogue = false;
            quester.isQuestGiver = false;
            quester.ToggleNPCMark(0);
        }
    }

    public void MakeZombieVisible()
    {
        enemy.SetActive(true);
    }
}
