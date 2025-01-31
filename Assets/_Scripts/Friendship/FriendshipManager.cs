using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct FriendshipData
{
    public string friendName;
    public int friendshipValue;

    public float GetFriendValue()
    {
        float floatValue = friendshipValue / 100f;
        return floatValue;
    }
}

public class FriendshipManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] friendshipNames;
    [SerializeField] Slider[] friendshipMeters;
    [SerializeField] public FriendshipData[] friendships;
    [SerializeField] GameObject friendShipField;

    void Start()
    {
        UpdateFriendMeter();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            SetFriendMeterValue(0, 50);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            SetFriendMeterValue(0, 20);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            SetFriendMeterValue(0, 100);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            SetFriendMeterValue(0, 120);
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            friendShipField.SetActive(true);
        }
    }

    void SetFriendMeterValue(int friend, int newValue)
    {
        if (friend >= 0 && friend < friendships.Length)
        {
            friendships[friend].friendshipValue = newValue;
            UpdateFriendMeter();
        }
    }

    void UpdateFriendMeter()
    {
        for (int i = 0; i < friendshipMeters.Length; i++)
        {
            if (friendshipMeters.Length > i)
            {
                friendshipMeters[i].value = friendships[i].GetFriendValue();
            }
        }
    }
}