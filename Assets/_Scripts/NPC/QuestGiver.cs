using TMPro;
using UnityEngine;
using UnityEngine.AI;

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
        outlinething.enabled = false;
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
            GetComponent<NavMeshAgent>().isStopped = true;

            if (other.gameObject.CompareTag("Player"))
            {
                this.gameObject.transform.LookAt(other.transform.position);
                other.gameObject.transform.LookAt(this.gameObject.transform.position);
                if (other.gameObject.GetComponent<pPlayerControlls>())
                {
                    DialogueManager diaManager = other.gameObject.GetComponent<DialogueManager>();
                    QuestManager queManager = other.gameObject.GetComponent<QuestManager>();

                    diaManager.oppositeTalker = this.gameObject;
                    outlinething.enabled = true;
                   
                    Debug.Log(this.gameObject.name + " interacted with player " + queManager.currentQuest.isCompleted);
                    other.gameObject.GetComponent<pPlayerComponent>().canInteract = true;
                    other.gameObject.GetComponent<pPlayerComponent>().isInteracting = true;

                    if (!hasQuest)
                    {
                        diaManager.SetDialogueRef(dialogues[dialogueIndex]);
                    }
                    else
                    {
                        if (queManager.currentQuest.isCompleted)
                        {/*
                            if (queManager.questRef == this.questToGive[questIndex - 1])
                            {
                                
                            }
                            else
                            {*/
                                Debug.Log("QuestRef: not the correct quest person? current quest = " + queManager.questRef.quest.questName + " / quest of person talked to: " + this.questToGive[questIndex].quest.questName);
                                Debug.Log("currentquest: not the correct quest person? current quest = " + queManager.currentQuest.questName + " / quest of person talked to: " + this.questToGive[questIndex].quest.questName);
                        //    }
                        }
                    }
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
        GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void ToggleNPCMark()
    {
        npcMark.SetActive(hasQuestToGive);
    }
}