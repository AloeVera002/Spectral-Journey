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
                        other.gameObject.GetComponent<QuestManager>().ShowHideQuestUI();
                        other.gameObject.GetComponent<pPlayerComponent>().ectoplasm += other.gameObject.GetComponent<QuestManager>().currentQuest.ectoplasmReward;
                        other.gameObject.GetComponent<FriendshipManager>().friendships[0].IncreaseFriendValue(other.gameObject.GetComponent<QuestManager>().currentQuest.friendshipIncreaseValue);
                        other.gameObject.GetComponent<FriendshipManager>().UpdateFriendMeterExternalCall(0);
                        other.gameObject.GetComponent<pPlayerComponent>().UpdateText(other.gameObject.GetComponent<pPlayerComponent>().ectroplasmText, other.gameObject.GetComponent<pPlayerComponent>().ectoplasm.ToString());
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