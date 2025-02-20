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
    [SerializeField] int qObjectiveIndex = 0;

    [SerializeField] GameObject parentPickupStuff;

    [SerializeField] public AudioClip pickupSound;
    [SerializeField] public AudioClip completeObjectiveSound;

    [SerializeField] bool isToggled;

    public delegate void QuestObjectiveEvent();
    public event QuestObjectiveEvent OnQuestObjective;

    public delegate void CompleteQuestEvent();
    public event CompleteQuestEvent OnQuestComplete;

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
                other.gameObject.SetActive(false);

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
    }

    public void ShowQuestHUD()
    {
        isToggled = true;
        ShowHideQuestUINew();
    }

    public void HideQuestHUD()
    {
        isToggled = false;
        ShowHideQuestUINew();
    }

    public void ShowHideQuestUINew()
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

    public void ShowHideQuestUI()
    {
        Debug.Log(isToggled + "Called ShowHideQuest");
        if (!isToggled)
        {
            questField.SetActive(true);
        }
        else
        {
            questField.SetActive(false);
        }
        isToggled = !isToggled;
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
    {
        UpdateQuestDetails();
    }

    public void CompleteQuest()
    {
        Debug.Log(currentQuest.questName);
        currentQuest.isCompleted = true;
        GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(completeObjectiveSound);
        GetComponent<DialogueManager>().SetDialogueRef(currentQuest.CompletedQuestDialogue);
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().dialogueIndex++;
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questIndex++;
        if (GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questToGive[GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questIndex - 1] == questRef)
        {
            GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().hasQuest = true;
        }
        questDetailsText.text = "Completed quest go back to your quest giver";
    }

    void UpdateQuestObjective()
    {
        Debug.Log("Updated QuestObjective");
        Debug.Log(currentQuest.questName);

        if (qObjectiveIndex < currentQuest.questObjective.Length)
        {
            Debug.Log("updated Quest tihi " + currentQuest.questObjective.Length + " " + qObjectiveIndex);
            qObjectiveIndex++;
            UpdateQuestDetails();
            if (qObjectiveIndex == currentQuest.questObjective.Length)
            {
                Debug.Log("man quest index is finished " + currentQuest.questObjective.Length + " " + qObjectiveIndex);
                Debug.Log("completed quest!");
                CallQuestOCompleteEvent();
            }
        }
        else
        {
            Debug.Log("completed quest!");
            CallQuestOCompleteEvent();
        }
    }

    public bool InitializeQuest()
    {
        questRef = GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questToGive[GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questIndex];
        if (questRef == GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questToGive[GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questIndex])
        {
            return true;
        }
        else { return false; }
    }

    public void StartQuest()
    {
        if (InitializeQuest())
        {
            currentQuest = questRef.quest;
            qObjectiveIndex = 0;
            SetQuestData();
            ShowQuestHUD();
            //   ShowHideQuestUI();

            if (currentQuest.QuestType == QuestTypeEnum.Pickup)
            {
                parentPickupStuff.SetActive(true);
                foreach (Transform child in parentPickupStuff.transform)
                {
                    child.gameObject.SetActive(true);
                    Debug.Log("activated pearl?");
                }
            }
            else
            {
                parentPickupStuff.SetActive(false);
            }
        }
        Debug.Log(InitializeQuest());
    }

    void SetQuestData()
    {
        Debug.Log("Set quest data");

        questObjectives = new GameObject[0];
        questObjectives = currentQuest.questObjective;
        UpdateQuestDetails();
    }

    public void ResetQuestObjectives()
    {
        questObjectives = new GameObject[0];
    }

    void UpdateQuestDetails()
    {
        string finalOutput = currentQuest.questDetails;
        finalOutput = GetComponent<DialogueManager>().ReplacePlaceholderText(finalOutput, "{i}", qObjectiveIndex.ToString());
        finalOutput = GetComponent<DialogueManager>().ReplacePlaceholderText(finalOutput, "{o}", (questObjectives.Length).ToString());

        questDetailsText.text = finalOutput;
    }

    public void UpdateFriendMeter()
    {
        FriendshipData targetFriendshipData = GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().friendshipData;

        // Find the index of the targetFriendshipData in the friendships array
        int friendIndex = -1;
        for (int i = 0; i < GetComponent<FriendshipManager>().friendships.Length; i++)
        {
            if (GetComponent<FriendshipManager>().friendships[i].friendName == targetFriendshipData.friendName)
            {
                friendIndex = i;
                break;
            }
        }

        // If a matching FriendshipData was found, update it
        if (friendIndex != -1)
        {
            // Increase the friendship value
            GetComponent<FriendshipManager>().friendships[friendIndex].IncreaseFriendValue(currentQuest.friendshipIncreaseValue);

            // Update the corresponding Friendship meter (slider)
            GetComponent<FriendshipManager>().UpdateFriendMeterExternalCall(friendIndex);
        }
        else
        {
            Debug.LogError("Friendship data not found for: " + targetFriendshipData.friendName);
        }
    }
}