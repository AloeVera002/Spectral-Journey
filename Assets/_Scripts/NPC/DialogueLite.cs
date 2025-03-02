using UnityEngine;

public class DialogueLite : MonoBehaviour
{
    [SerializeField] so_Dialogue startDialogue;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<DialogueManager>().SetDialogueRef(startDialogue);
            other.GetComponent<DialogueManager>().StartNewDialogue();
            Invoke("DestroyThis", 10);
        }
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
