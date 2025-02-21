using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public string objName;
    public string itemTag;
    public QuestTypeEnum questType;

    void Start()
    {
        this.tag = itemTag;
    }

    void Update()
    {
    }
}
