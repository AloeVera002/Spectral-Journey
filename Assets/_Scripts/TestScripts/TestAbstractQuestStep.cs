using UnityEngine;

public abstract class TestAbstractQuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            // advance
            Destroy(this.gameObject);
        }
    }
}
