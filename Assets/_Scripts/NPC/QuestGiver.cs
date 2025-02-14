using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] public QuestScriptableObj questToGive;
    [SerializeField] so_Dialogue[] Dialogue;
    [SerializeField] bool isQuestGiver;
    [SerializeField] public FriendshipData friendshipData;

    [SerializeField] public int dialogueIndex;
    [SerializeField] public bool hasQuest;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isQuestGiver)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<pPlayerControlls>())
                {
                    if (!hasQuest)
                    {
                        other.gameObject.GetComponent<DialogueManager>().SetDialogueRef(Dialogue[dialogueIndex]);
                        other.gameObject.GetComponent<DialogueManager>().oppositeTalker = this.gameObject;
                    }
                    else
                    {
                        Debug.Log("Already Has Dialogue");
                    }

                    other.gameObject.GetComponent<pPlayerComponent>().isInteracting = true;
                    other.gameObject.GetComponent<DialogueManager>().StartNewDialogue();

                    other.gameObject.GetComponent<QuestManager>().questRef = questToGive;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }
}