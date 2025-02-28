using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public QuestScriptableObj[] questToGive;
    [SerializeField] so_Dialogue[] dialogues;
    [SerializeField] bool isQuestGiver;
    [SerializeField] bool hasDialogue;
    [SerializeField] float timeToTurn = .5f;
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
        Debug.Log(outlinething.name);
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

            GetComponent<NavMeshAgent>().isStopped = true;
            outlinething.enabled = true;

        //    StartCoroutine(FaceSomeone(this.gameObject, other.gameObject, timeToTurn));
            other.gameObject.transform.LookAt(this.gameObject.transform.position);
            this.gameObject.transform.LookAt(other.gameObject.transform.position);

            if (hasDialogue)
            {
                if (!dontGiveNewDialogue)
                {
                    diaManager.SetDialogueRef(dialogues[dialogueIndex]);
                }
            }

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
                Debug.Log("Quest index to check: " + questIndexToCheck + " = " + questToGive[questIndexToCheck].name);

                if (queManager.questRef == this.questToGive[questIndexToCheck] && queManager.currentQuest.QuestType == QuestTypeEnum.Engage)
                {
                    queManager.CallQuestObjectiveEvent();
                }
                if (queManager.currentQuest.isCompleted && queManager.currentQuest.QuestType == QuestTypeEnum.Deliver)
                {
                    queManager.CallQuestOCompleteEvent();
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

    public void ToggleNPCMark(int index, bool inBool)
    {
        npcMarks[index].SetActive(inBool);
    }

    IEnumerator FaceSomeone(GameObject personToTurn, GameObject personToTurnTo, float timeToRotate)
    {
        Quaternion startRotation = personToTurn.transform.rotation;
        Vector3 direction = personToTurnTo.transform.position - personToTurn.transform.position;
        direction.y = 0;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);

        float elapsedTime = 0;
        while (elapsedTime < timeToRotate)
        {
            personToTurn.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(timeToRotate);
        }
        personToTurn.transform.rotation = endRotation;
    }
}