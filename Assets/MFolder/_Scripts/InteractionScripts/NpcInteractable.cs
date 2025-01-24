using UnityEngine;

public class NpcInteractable : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;

    private bool isPlayerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKey(KeyCode.E)){
            dialogueBox.SetActive(true);
        }
    }

  

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other){
        isPlayerInRange = false;
    }


}
