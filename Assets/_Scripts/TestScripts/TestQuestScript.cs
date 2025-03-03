using UnityEngine;


[System.Serializable]
public enum ETestQuestState
{
    NotStarted,
    Ongoing,
    Completed
}

public class TestQuestScript : MonoBehaviour
{
    public so_Quest info;
    public ETestQuestState state;
    private int currentQuestStepIndex;

    public TestQuestScript(so_Quest questInfo)
    {
        this.info = questInfo;
        this.state = ETestQuestState.NotStarted;
        this.currentQuestStepIndex = 0;
    }

    public void AdvanceToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExist()
    {
        return (currentQuestStepIndex < info.step.Length);
    }

    public void InstantiateCurrentQuestStep(Transform ptransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            questStepPrefab = Instantiate<GameObject>(questStepPrefab, ptransform);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questSPrefab = null;
        if (CurrentStepExist())
        {
            questSPrefab = info.step[currentQuestStepIndex];
        }
        else
        {
            Debug.Log("Quest Step Prefab Index out of range");
        }
        return questSPrefab;
    }
}
