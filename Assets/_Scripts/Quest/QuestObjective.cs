using UnityEngine;

[System.Serializable]
public enum EQuestInteractionType
{
    touch,
    interact
}

public class QuestObjective : MonoBehaviour
{
    public string objectName;
    public string objectTag;
    public EQuestInteractionType questType = EQuestInteractionType.touch;

    void Start()
    {
        if (objectName != string.Empty || objectTag != string.Empty)
        {
            this.gameObject.tag = objectTag;
        }
        else
        {
            objectName = this.gameObject.tag;
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (questType == EQuestInteractionType.touch)
            {
                other.gameObject.GetComponent<QuestManager>().isTouch = true;
                Debug.Log("quest object is Touch + " + other.gameObject.GetComponent<QuestManager>().isTouch);
                other.gameObject.GetComponent<QuestManager>().HandleObjectiveInteractionByGameObject(this.gameObject);
            }
            else if (questType == EQuestInteractionType.interact)
            {
                other.gameObject.GetComponent<QuestManager>().isTouch = false;
                other.gameObject.GetComponent<QuestManager>().canInteractWith = true;
                Debug.Log("quest object is not Touch + " + other.gameObject.GetComponent<QuestManager>().isTouch + " and can interactw: " + other.gameObject.GetComponent<QuestManager>().canInteractWith);
                other.gameObject.GetComponent<QuestManager>().SetObjectiveToInteractWith(this.gameObject);
            }
        }
    }
}
