using System.Data;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestScriptableObj questRef;
    [SerializeField] GameObject questField;
    basicQuest currentQuest;
    [SerializeField] GameObject[] questObjectives;
    [SerializeField] TMP_Text questDetailsText, questCompletionValue;
    [SerializeField] bool hasCompletedObjective;
    int questIndex;
    int qObjectiveIndex = 0;

    [SerializeField] bool isToggled;
    /*
    public QuestScriptableObj GetElementByIdentifier(string identifier)
    {
        foreach (var BasicQuest in CurrentQuest.quests)
        {
            if (basicQuest.identifier == identifier)
            {
                return element;
            }
        }
        Debug.LogWarning($"Element with identifier '{identifier}' not found.");
        return null;
    }*/

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (qObjectiveIndex <= currentQuest.questObjective.Length)
        {
            if (other.gameObject == currentQuest.questObjective[qObjectiveIndex])
            {
                Destroy(other.gameObject);
                qObjectiveIndex++;
                UpdateQuest();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            ShowHideQuestUI();
        }
    }

    public void ShowHideQuestUI()
    {
        if (!isToggled)
        {
            questField.SetActive(true);
        }
        else
        {
            questField.SetActive(false);
        }
    }

    public void UpdateQuest()
    {
        currentQuest.questProgress = 1;
        if (questObjectives.Length <= 0)
        {
            Debug.Log("quest");
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
        GetComponentInParent<pPlayerComponent>().ectoplasm += currentQuest.ectoplasmReward;
    }

    void StartQuest()
    {
        SetQuestData();
        ShowHideQuestUI();
    }

    void SetQuestData()
    {
        questDetailsText.text = currentQuest.questDetails;
        questObjectives = currentQuest.questObjective;
    }
}
