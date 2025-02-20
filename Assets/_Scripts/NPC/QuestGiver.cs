using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public QuestScriptableObj[] questToGive;
    [SerializeField] so_Dialogue[] dialogues;
    [SerializeField] bool isQuestGiver;
    bool hasQuestToGive = false;
    [SerializeField] GameObject npcMark;
    [SerializeField] public FriendshipData friendshipData;

    [SerializeField] public int dialogueIndex;
    [SerializeField] public int questIndex;
    [SerializeField] public bool hasQuest;
    private Outline outlinething;

    void Start()
    {
        if (isQuestGiver) { hasQuestToGive = true; ToggleNPCMark(); }
        else { hasQuestToGive = false; ToggleNPCMark(); }
        outlinething = GetComponent<Outline>();
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
                    outlinething.enabled = true;
                    DialogueManager diaManager = other.gameObject.GetComponent<DialogueManager>();
                    QuestManager queManager = other.gameObject.GetComponent<QuestManager>();
                    FriendshipManager freManager = other.gameObject.GetComponent<FriendshipManager>();
                    Debug.Log(this.gameObject.name + " interacted with player " + queManager.currentQuest.isCompleted);
                    other.gameObject.GetComponent<pPlayerComponent>().canInteract = true;

                    if (!hasQuest)
                    {
                        diaManager.SetDialogueRef(dialogues[dialogueIndex]);
                    }
                    else
                    {
                        if (!queManager.questRef)
                        {
                            queManager.HideQuestHUD();
                        }
                        if (queManager.currentQuest.isCompleted)
                        {
                            if (queManager.questRef == this.questToGive[questIndex - 1])
                            {
                                other.gameObject.GetComponent<pPlayerComponent>().ectoplasm += other.gameObject.GetComponent<QuestManager>().currentQuest.ectoplasmReward;
                                other.gameObject.GetComponent<pPlayerComponent>().UpdateText(other.gameObject.GetComponent<pPlayerComponent>().ectroplasmText, other.gameObject.GetComponent<pPlayerComponent>().ectoplasm.ToString());

                                queManager.UpdateFriendMeter();

                                queManager.currentQuest = new basicQuest();
                                queManager.ResetQuestObjectives();
                            }
                            else
                            {
                                Debug.Log("QuestRef: not the correct quest person? current quest = " + queManager.questRef.quest.questName + " / quest of person talked to: " + this.questToGive[questIndex].quest.questName);
                                Debug.Log("currentquest: not the correct quest person? current quest = " + queManager.currentQuest.questName + " / quest of person talked to: " + this.questToGive[questIndex].quest.questName);
                            }
                        }
                    }

                    diaManager.oppositeTalker = this.gameObject;
                    other.gameObject.GetComponent<pPlayerComponent>().isInteracting = true;
                //    diaManager.StartNewDialogue();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerV2")
        {
            other.gameObject.GetComponent<pPlayerComponent>().canInteract = false;
        }
        outlinething.enabled = false;
    }

    public void ToggleNPCMark()
    {
        npcMark.SetActive(hasQuestToGive);
    }
}