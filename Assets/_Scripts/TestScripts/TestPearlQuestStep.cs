using UnityEngine;

public class TestPearlQuestStep : TestAbstractQuestStep
{
    private int collectedCoins = 0;
    private int coinsToComplete = 5;

    private void OnEnable()
    {
        TestGameEventsManager.instance.miscEvents.onCoinCollected += CollectedCoins;
    }

    private void OnDisable()
    {
        TestGameEventsManager.instance.miscEvents.onCoinCollected -= CollectedCoins;
    }

    private void CollectedCoins()
    {
        if (collectedCoins < coinsToComplete)
        {
            collectedCoins++;
        }

        if (collectedCoins >= coinsToComplete)
        {
            FinishQuestStep();
        }
    }
}
