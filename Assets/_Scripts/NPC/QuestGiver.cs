using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public QuestScriptableObj questToGive;
    [SerializeField] so_Dialogue[] dialogues;
    [SerializeField] bool isQuestGiver;
    bool hasQuestToGive = false;
    [SerializeField] GameObject npcMark;
    [SerializeField] public FriendshipData friendshipData;

    [SerializeField] public int dialogueIndex;
    [SerializeField] public bool hasQuest;

    void Start()
    {
        if (isQuestGiver) { hasQuestToGive = true; ToggleNPCMark(); }
    }

    void Update()
    {
        if (dialogueIndex > dialogues.Length)
        {
            hasQuestToGive = false;
            ToggleNPCMark();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isQuestGiver)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<pPlayerControlls>())
                {
                    DialogueManager diaManager = other.gameObject.GetComponent<DialogueManager>();
                    QuestManager queManager = other.gameObject.GetComponent<QuestManager>();
                    FriendshipManager freManager = other.gameObject.GetComponent<FriendshipManager>();

                    if (!hasQuest)
                    {
                        diaManager.SetDialogueRef(dialogues[dialogueIndex]);
                        diaManager.oppositeTalker = this.gameObject;
                    }
                    else
                    {
                        queManager.ShowHideQuestUI();
                        other.gameObject.GetComponent<pPlayerComponent>().ectoplasm += other.gameObject.GetComponent<QuestManager>().currentQuest.ectoplasmReward;
                        other.gameObject.GetComponent<pPlayerComponent>().UpdateText(other.gameObject.GetComponent<pPlayerComponent>().ectroplasmText, other.gameObject.GetComponent<pPlayerComponent>().ectoplasm.ToString());

                        FriendshipData targetFriendshipData = this.friendshipData;

                        // Find the index of the targetFriendshipData in the friendships array
                        int friendIndex = -1;
                        for (int i = 0; i < freManager.friendships.Length; i++)
                        {
                            if (freManager.friendships[i].friendName == targetFriendshipData.friendName)
                            {
                                friendIndex = i;
                                break;
                            }
                        }

                        // If a matching FriendshipData was found, update it
                        if (friendIndex != -1)
                        {
                            // Increase the friendship value
                            freManager.friendships[friendIndex].IncreaseFriendValue(queManager.currentQuest.friendshipIncreaseValue);

                            // Update the corresponding Friendship meter (slider)
                            freManager.UpdateFriendMeterExternalCall(friendIndex);
                        }
                        else
                        {
                            Debug.LogError("Friendship data not found for: " + targetFriendshipData.friendName);
                        }
                        /*
                        freManager.friendships[0].IncreaseFriendValue(other.gameObject.GetComponent<QuestManager>().currentQuest.friendshipIncreaseValue);
                        freManager.UpdateFriendMeterExternalCall(0);*/
                    }

                    other.gameObject.GetComponent<pPlayerComponent>().isInteracting = true;
                    diaManager.StartNewDialogue();

                    queManager.questRef = questToGive;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }

    public void ToggleNPCMark()
    {
        npcMark.SetActive(hasQuestToGive);
    }
}