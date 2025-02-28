using System.Data;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public QuestScriptableObj questRef;
    [SerializeField] GameObject questField;
    public basicQuest currentQuest;
    [SerializeField] GameObject[] questObjectives;
    [SerializeField] TMP_Text questDetailsText;
    [SerializeField] bool hasCompletedObjective;
    [SerializeField] int qObjectiveIndex = 0;
    [SerializeField] public bool ongoingQuest;

    [SerializeField] AudioClip questRewardSound;

    [SerializeField] public bool isTouch;
    [SerializeField] public bool canInteractWith;

    [SerializeField] GameObject messageHUD;
    [SerializeField] Sprite[] messageHUDSprites;
    int spriteIndex = 0;
    float timerMessageHud = 0f;

    [SerializeField] Transform[] spawnLocations;
    [SerializeField] GameObject[] questItemsToSpawn;
    [SerializeField] List<int> usedLocations = new List<int>();

    [SerializeField] GameObject parentPickupStuff;

    [SerializeField] public AudioClip pickupSound;
    [SerializeField] public AudioClip completeObjectiveSound;

    [SerializeField] bool isToggled;

    public delegate void QuestObjectiveEvent();
    public event QuestObjectiveEvent OnQuestObjective;

    public delegate void CompleteQuestEvent();
    public event CompleteQuestEvent OnQuestComplete;

    private GameObject targetToDisapear, objtar;

    void Start()
    {
        OnQuestObjective += UpdateQuestObjective;
        OnQuestComplete += CompleteQuest;

        // remember to remove
        GameObject.Find("Enemy").SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //    HandleObjectiveInteraction(other);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))// && !isTouch)
        {
            if (canInteractWith)
            {
                Debug.Log("called stuff with e");
                InteractWithObjectiveByGameObject(objtar);
            }
        }
        /*
        if (messageHUD.activeInHierarchy)
        {
            if (timerMessageHud >= 10f)
            {
                timerMessageHud = 0f;
                Destroy(messageHUD);
            }
            timerMessageHud += Time.deltaTime;
        }*/
    }

    public void SetObjectiveToInteractWith(GameObject gameobject)
    {
        objtar = gameobject;
        Debug.Log("set orb to: " + objtar);
    }

    public void HandleObjectiveInteraction(Collider other)
    {
        if (isTouch)
        {
            InteractWithObjective(other);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithObjective(other);
            }
        }
    }

    public void HandleObjectiveInteractionByGameObject(GameObject other)
    {
        Debug.Log("called handle interact by gameobj");
        if (isTouch)
        {
            Debug.Log("was touch");
            InteractWithObjectiveByGameObject(other);
        }/*
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithObjectiveByGameObject(other);
            }
        }*/
    }

    void InteractWithObjective(Collider other)
    {
        Debug.Log("called Interact");
        if (qObjectiveIndex < currentQuest.questObjectives.Length)
        {
            if (other.gameObject.GetComponent<QuestObjective>().objectName == currentQuest.pickupTag)
            {
                Debug.Log("Picked up a QuestObjective");
                other.gameObject.SetActive(false);

                CallQuestObjectiveEvent();

                GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(pickupSound);
            }
        }
    }

    void InteractWithObjectiveByGameObject(GameObject other)
    {
        Debug.Log("called Interact");
        if (qObjectiveIndex < currentQuest.questObjectives.Length)
        {
            if (other.GetComponent<QuestObjective>().objectName == currentQuest.pickupTag)
            {
                if (currentQuest.QuestType == QuestTypeEnum.Deliver)
                {
                    if (qObjectiveIndex == currentQuest.questObjectives.Length)
                    { currentQuest.isCompleted = true; }
                }
                Debug.Log("Picked up a QuestObjective");
                other.SetActive(false);

                CallQuestObjectiveEvent();

                GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(pickupSound);

            }
        }
    }

    void InitializeQuestPickups(int questObjectivesAmount)
    {
        for (int i = 0; i < questObjectivesAmount; i++)
        {
            int randomLocation = GetRandomAvailableLocation();
            if (randomLocation == -1)
            {
                break;
            }

            GameObject questObjectivePrefab =// questItemsToSpawn[Random.Range(0, questItemsToSpawn.Length)];
            Instantiate(questItemsToSpawn[0], spawnLocations[randomLocation].position, spawnLocations[randomLocation].rotation);
            questObjectivePrefab.transform.parent = parentPickupStuff.transform;

            usedLocations.Add(randomLocation);
        }
    }

    private int GetRandomAvailableLocation()
    {
        List<int> availableLocations = new List<int>();

        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (!usedLocations.Contains(i))
            {
                availableLocations.Add(i);
            }
        }

        if (availableLocations.Count == 0)
        {
            return -1;
        }

        int randomIndex = availableLocations[Random.Range(0, availableLocations.Count)];
        return randomIndex;
    }

    public void ShowQuestHUD()
    {
        Debug.Log("showed quest ui");
        isToggled = true;
        ShowHideQuestUINew();
    }

    public void HideQuestHUD()
    {
        Debug.Log("Hid quest ui");
        isToggled = false;
        ShowHideQuestUINew();
    }

    public void ShowHideQuestUINew()
    {
        if (isToggled)
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
        Debug.Log("Completed quest: " + currentQuest.questName);
        currentQuest.isCompleted = true;
        GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(completeObjectiveSound);
        GetComponent<DialogueManager>().SetDialogueRef(currentQuest.CompletedQuestDialogue);
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().dialogueIndex++;
        GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questIndex++;
        if (GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questToGive[GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questIndex - 1] == questRef)
        {
            GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().dontGiveNewDialogue = true;
        }
        questDetailsText.text = "Quest completed! Go and collect your reward";
    }

    public void NextPageTutorialHUD()
    {
        Time.timeScale = 0;
        if (spriteIndex < messageHUDSprites.Length)
        {
            spriteIndex++;
        }
        else
        {
            messageHUD.SetActive(false);
            Time.timeScale = 1;
        }
        messageHUD.GetComponent<Image>().sprite = messageHUDSprites[spriteIndex];
    }

    public void GiveQuestReward()
    {
        GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(questRewardSound);
        if (questRef == GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questToGive[(GetComponent<DialogueManager>().oppositeTalker.GetComponent<QuestGiver>().questIndex - 1)])
        {
            GetComponent<pPlayerComponent>().ectoplasm += GetComponent<QuestManager>().currentQuest.questReward.ectoplasmReward;
            GetComponent<pPlayerComponent>().UpdateText(GetComponent<pPlayerComponent>().ectroplasmText, GetComponent<pPlayerComponent>().ectoplasm.ToString());

            targetToDisapear = GetComponent<DialogueManager>().oppositeTalker;

            UpdateFriendMeter();

            currentQuest = new basicQuest();
            ResetQuestObjectives();

            if (currentQuest.isTutorialQuest)
            {
                GetComponent<pPlayerComponent>().tutorialQuestDone = true;
                GetComponent<pPlayerComponent>().slingshot.SetActive(true);
                messageHUD.SetActive(true);
                GetComponent<pPlayerComponent>().InitPebblesHUD();
            }
        }
        Invoke("DisapeariousGhostus", 5);
    }

    void DisapeariousGhostus()
    {
        Destroy(targetToDisapear.gameObject);
        targetToDisapear = null;
    }

    void UpdateQuestObjective()
    {
        Debug.Log("Updated QuestObjective");
        Debug.Log(currentQuest.questName);

        if (qObjectiveIndex < currentQuest.questObjectives.Length)
        {
            Debug.Log("updated Quest tihi " + currentQuest.questObjectives.Length + " " + qObjectiveIndex);
            qObjectiveIndex++;
            UpdateQuestDetails();
            if (qObjectiveIndex == currentQuest.questObjectives.Length)
            {
                if (!(currentQuest.QuestType == QuestTypeEnum.Deliver))
                {
                    Debug.Log("man quest index is finished " + currentQuest.questObjectives.Length + " " + qObjectiveIndex);
                    Debug.Log("completed quest!");
                    CallQuestOCompleteEvent();
                }
                else
                {
                    questDetailsText.text = "Go back to talk to quest giver to finish";
                }
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
        return false;
    }

    public void StartQuest()
    {
        if (!ongoingQuest)
        {
            ongoingQuest = true;
        }
        if (InitializeQuest())
        {
            currentQuest = questRef.quest;
            qObjectiveIndex = 0;
            SetQuestData();
            ShowQuestHUD();

            if (currentQuest.QuestType == QuestTypeEnum.Pickup)
            {
                parentPickupStuff.SetActive(true);
                InitializeQuestPickups(currentQuest.questObjectives.Length);
            }
            else
            {
                parentPickupStuff.SetActive(false);
            }
            if (currentQuest.QuestType == QuestTypeEnum.Pickup)
            {
                GameObject.Find("Enemy").SetActive(true);
            }
        }
        Debug.Log(InitializeQuest());
    }

    void SetQuestData()
    {
        Debug.Log("Set quest data");

        questObjectives = new GameObject[0];
        questObjectives = currentQuest.questObjectives;
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
            GetComponent<FriendshipManager>().friendships[friendIndex].IncreaseFriendValue(currentQuest.questReward.friendReward);

            // Update the corresponding Friendship meter (slider)
            GetComponent<FriendshipManager>().UpdateFriendMeterExternalCall(friendIndex);
        }
        else
        {
            Debug.LogError("Friendship data not found for: " + targetFriendshipData.friendName);
        }
    }
}