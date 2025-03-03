using UnityEngine;

public class TestGameEventsManager : MonoBehaviour
{
    public static TestGameEventsManager instance { get; private set; }

    public TestMiscEvents miscEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("TestGameEventsManager already exists");
        }
        instance = this;

        miscEvents = new TestMiscEvents();
    }
}
