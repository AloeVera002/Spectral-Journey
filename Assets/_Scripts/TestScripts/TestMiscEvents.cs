using System;

public class TestMiscEvents
{
    public event Action onCoinCollected;
    public void CollectCoin()
    {
        onCoinCollected?.Invoke();
    }
}