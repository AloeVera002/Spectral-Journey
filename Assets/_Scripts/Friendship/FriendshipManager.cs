using System.Collections;
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
        QuestGiver[] newFriends = Object.FindObjectsByType<QuestGiver>(FindObjectsSortMode.None);;
        friendships = new FriendshipData[newFriends.Length];

        for (int i = 0; i < newFriends.Length; i++)
        {
            friendships[i] = newFriends[i].friendshipData;
        }
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
            StartCoroutine(SmoothUpdateFriendMeter(friend));
        //    UpdateFriendMeter();
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

    IEnumerator SmoothUpdateFriendMeter(int friend)
    {
        float targetValue = friendships[friend].GetFriendValue();
        float currentValue = friendshipMeters[friend].value;

        // Smoothly transition from the current value to the target value
        while (!Mathf.Approximately(currentValue, targetValue))
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, 1f * Time.deltaTime);
            friendshipMeters[friend].value = currentValue;
            yield return null; // Wait until the next frame
        }

        // Ensure the target value is exactly reached
        friendshipMeters[friend].value = targetValue;
    }
}