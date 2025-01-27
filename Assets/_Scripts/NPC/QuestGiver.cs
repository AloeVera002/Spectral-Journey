using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] QuestScriptableObj questToGive;
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
                other.gameObject.GetComponent<DialogueManager>().StartDialogue();
            }
        }
    }
}
