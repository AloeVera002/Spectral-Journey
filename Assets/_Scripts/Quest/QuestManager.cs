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
        if (qObjectiveIndex < currentQuest.questObjective.Length)
        {
            if (other.gameObject.tag == currentQuest.pickupTag)
            {
                Destroy(other.gameObject);
                GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(pickupSound);
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

    public void CompleteQuest()
    {
        GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(completeObjectiveSound);
        GetComponent<pPlayerComponent>().ectoplasm += currentQuest.ectoplasmReward;
        GetComponent<DialogueManager>().SetDialogueRef(currentQuest.CompletedQuestDialogue);
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().dialogueIndex++;
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().hasQuest = true;
        GetComponent<FriendshipManager>().friendships[0].IncreaseFriendValue(currentQuest.friendshipIncreaseValue);
        GetComponent<FriendshipManager>().UpdateFriendMeterExternalCall(0);
        questDetailsText.text = "Completed quest go back to your quest giver";
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