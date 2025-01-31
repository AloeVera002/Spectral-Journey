using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] QuestScriptableObj questToGive;
    [SerializeField] so_Dialogue Dialogue;
    [SerializeField] bool isQuestGiver;

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
                    other.gameObject.GetComponent<DialogueManager>().SetDialogueRef(Dialogue);
                    other.gameObject.GetComponent<DialogueManager>().StartNewDialogue();

                    other.gameObject.GetComponent<QuestManager>().questRef = questToGive;
                }
            }
        }
    }
}
