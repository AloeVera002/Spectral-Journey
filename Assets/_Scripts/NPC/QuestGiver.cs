using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public QuestScriptableObj[] questToGive;
    [SerializeField] so_Dialogue[] dialogues;
    [SerializeField] bool isQuestGiver;
    [SerializeField] bool hasDialogue;
    bool hasQuestToGive = false;

    [SerializeField] GameObject[] npcMarks;
    [SerializeField] public FriendshipData friendshipData;

    [SerializeField] public int dialogueIndex;
    [SerializeField] public int questIndex;
    [SerializeField] public bool dontGiveNewDialogue;
    private Outline outlinething;

    void Start()
    {
        if (isQuestGiver) { hasQuestToGive = true; ToggleNPCMark(0, hasQuestToGive); }
        else { hasQuestToGive = false; ToggleNPCMark(0, hasQuestToGive); }

        outlinething = GetComponent<Outline>();
        outlinething.enabled = false;
    }

    void Update()
    {
        if (dialogueIndex > dialogues.Length)
        {
            hasQuestToGive = false;
            ToggleNPCMark(0, hasQuestToGive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            QuestManager queManager = other.gameObject.GetComponent<QuestManager>();
            DialogueManager diaManager = other.gameObject.GetComponent<DialogueManager>();

            other.gameObject.GetComponent<pPlayerComponent>().canInteract = true;
            diaManager.oppositeTalker = this.gameObject;
            /*
            GetComponent<NavMeshAgent>().isStopped = true;
            outlinething.enabled = true;

            float rotationSpeed = 6 * Time.deltaTime;
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(other.transform.position), rotationSpeed);
            other.gameObject.transform.rotation = Quaternion.Slerp(other.gameObject.transform.rotation, Quaternion.LookRotation(this.transform.position), rotationSpeed);*/ // other.gameObject.transform.LookAt(this.gameObject.transform.position);

            if (hasDialogue)
            {
                Debug.Log("Dont give dialogue bool: before: " + dontGiveNewDialogue);
                if (!dontGiveNewDialogue)
                {
                    Debug.Log("!DontGIVE NEW DIALOGUE");
                    diaManager.SetDialogueRef(dialogues[dialogueIndex]);
                }
                else
                {
                    Debug.Log("Dont give dialogue bool: " + dontGiveNewDialogue);
                }
                Debug.Log("Dont give dialogue bool: after: " + dontGiveNewDialogue);
            }
            /*
            if (isQuestGiver)
            {
                int questIndexToCheck;

                Debug.Log(this.gameObject.name + " interacted with player " + queManager.currentQuest.isCompleted);

                if (!(questIndex < 0))
                {
                    questIndexToCheck = questIndex - 1;
                }
                else
                {
                    questIndexToCheck = 0;
                }

                if (queManager.questRef == this.questToGive[questIndexToCheck] && queManager.currentQuest.QuestType == QuestTypeEnum.Engage)
                {
                    queManager.CallQuestObjectiveEvent();
                }
            }*/
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

    public void ToggleNPCMark(int index, bool inBool)
    {
        npcMarks[index].SetActive(inBool);
    }
}