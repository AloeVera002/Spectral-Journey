using System.Data;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestScriptableObj questRef;
    [SerializeField] GameObject questField;
    public basicQuest currentQuest;
    [SerializeField] GameObject[] questObjectives;
    [SerializeField] TMP_Text questDetailsText;
    [SerializeField] bool hasCompletedObjective;
    int qObjectiveIndex = 0;

    [SerializeField] public AudioClip pickupSound;
    [SerializeField] public AudioClip completeObjectiveSound;

    [SerializeField] bool isToggled;

    public delegate void QuestObjectiveEvent();
    public event QuestObjectiveEvent OnQuestObjective;

    public delegate void CompleteQuestEvent();
    public event CompleteQuestEvent OnQuestComplete;
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
        OnQuestObjective += UpdateQuestObjective;
        OnQuestComplete += CompleteQuest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (qObjectiveIndex < currentQuest.questObjective.Length)
        {
            if (other.gameObject.tag == currentQuest.pickupTag)
            {
                Debug.Log("Picked up a QuestObjective");
                Destroy(other.gameObject);

                CallQuestObjectiveEvent();

                GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(pickupSound);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            ShowHideQuestUI();
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            CompleteQuest();
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

    public void CallQuestObjectiveEvent()
    {
        OnQuestObjective?.Invoke();
    }

    public void CallQuestOCompleteEvent()
    {
        OnQuestComplete?.Invoke();
    }

    public void UpdateQuest()
    {/*
        currentQuest.questProgress = 1;
        if (questObjectives.Length <= 0)
        {
            Debug.Log("quest");
            CompleteQuest();
        }*/
        UpdateQuestDetails();
    }

    public void CompleteQuest()
    {
        currentQuest.isCompleted = true;
        GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(completeObjectiveSound);
        GetComponent<DialogueManager>().SetDialogueRef(currentQuest.CompletedQuestDialogue);
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().dialogueIndex++;
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().hasQuest = true;
        questDetailsText.text = "Completed quest go back to your quest giver";
    }

    void UpdateQuestObjective()
    {
        Debug.Log("Updated QuestObjective");
        qObjectiveIndex++;
        UpdateQuestDetails();
        if (qObjectiveIndex == currentQuest.questObjective.Length)
        {
            Debug.Log("completed quest!");
            CallQuestOCompleteEvent();
        }
    }

    public void StartQuest()
    {
        currentQuest = questRef.quest;
        SetQuestData();
        ShowHideQuestUI();
    }

    void SetQuestData()
    {
        Debug.Log("Set quest data");

        //   questDetailsText.text = currentQuest.questDetails;

        questObjectives = currentQuest.questObjective;
        UpdateQuestDetails();
    }

    void UpdateQuestDetails()
    {
        string finalOutput = currentQuest.questDetails;
        finalOutput = GetComponent<DialogueManager>().ReplacePlaceholderText(finalOutput, "{i}", qObjectiveIndex.ToString());
        finalOutput = GetComponent<DialogueManager>().ReplacePlaceholderText(finalOutput, "{o}", (questObjectives.Length).ToString());

        questDetailsText.text = finalOutput;
    }
}