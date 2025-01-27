using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestScriptableObj CurrentQuest;
    [SerializeField] GameObject questField;


    [SerializeField] bool isToggled;
    /*
    public QuestScriptableObj GetElementByIdentifier(string identifier)
    {
        foreach (var BasicQuest in CurrentQuest.quests)
        {
            if (basicQuest.identifier == identifier)
            {
                return element;
            }
        }
        Debug.LogWarning($"Element with identifier '{identifier}' not found.");
        return null;
    }*/

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            ShowHideQuestUI();
        }
    }

    public void ShowHideQuestUI()
    {
        if (!isToggled)
        {
            questField.SetActive(true);
        }
        else
        {
            questField.SetActive(false);
        }
    }
}
