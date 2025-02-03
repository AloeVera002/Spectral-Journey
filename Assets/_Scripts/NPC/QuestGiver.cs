using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] QuestScriptableObj questToGive;
    [SerializeField] so_Dialogue Dialogue;
    [SerializeField] bool isQuestGiver;
    [SerializeField] GameObject interactHUD;

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
                    other.gameObject.GetComponent<pPlayerComponent>().isInteracting = true;
                //    other.gameObject.GetComponent<DialogueManager>().StartNewDialogue();

                    other.gameObject.GetComponent<QuestManager>().questRef = questToGive;

                    interactHUD.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactHUD?.SetActive(false);
    }
}