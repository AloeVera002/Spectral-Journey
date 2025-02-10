using System.Data;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestScriptableObj questRef;
    [SerializeField] GameObject questField;
    basicQuest currentQuest;
    [SerializeField] GameObject[] questObjectives;
    [SerializeField] TMP_Text questDetailsText;
    [SerializeField] bool hasCompletedObjective;
    int qObjectiveIndex = 0;

    [SerializeField] AudioClip pickupSound;
    [SerializeField] AudioSource pickupAudioSource;

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
        pickupAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (qObjectiveIndex < currentQuest.questObjective.Length)
        {
            if (other.gameObject.tag == currentQuest.pickupTag)
            {
                Destroy(other.gameObject);
                pickupAudioSource.PlayOneShot(pickupSound);
                qObjectiveIndex++;
                UpdateQuest();
                if (qObjectiveIndex == currentQuest.questObjective.Length)
                {
                    currentQuest.isCompleted = true;
                    Debug.Log("completed quest!");
                    CompleteQuest();
                }
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

    void CompleteQuest()
    {
        GetComponentInParent<pPlayerComponent>().ectoplasm += currentQuest.ectoplasmReward;
        GetComponent<DialogueManager>().SetDialogueRef(currentQuest.CompletedQuestDialogue);
        questDetailsText.text = "Completed quest here is 10 Ectoplasm!";
    }

    public void StartQuest()
    {
        currentQuest = questRef.quest;
        SetQuestData();
        ShowHideQuestUI();
        //GetComponent<FriendshipManager>().friendships;
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