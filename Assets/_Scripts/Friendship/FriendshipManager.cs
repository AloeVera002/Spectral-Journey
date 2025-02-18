using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public struct FriendshipData
{
    public string friendName;
    public int friendshipValue;
    public bool isActive;

    public float GetFriendValue()
    {
        float floatValue = friendshipValue / 100f;
        return floatValue;
    }

    public void SetFriendValue(int newValue)
    {
        friendshipValue = newValue;
    }

    public void IncreaseFriendValue(int incValue)
    {
        friendshipValue += incValue;
    }
}

public class FriendshipManager : MonoBehaviour
{
    [SerializeField] GameObject friendShipPrefab;
    [SerializeField] public FriendshipData[] friendships;
    [SerializeField] private List<TMP_Text> friendshipNames = new List<TMP_Text>();
    [SerializeField] private List<Slider> friendshipMeters = new List<Slider>();
    [SerializeField] public Sprite[] friendMeterImages;

    [SerializeField] private GameObject friendShipField;

    void Start()
    {
        InitiateFriendshipMeters();
        //   UpdateFriendMeter();
    }

    void Update()
    {
    }

    void InitiateFriendshipMeters()
    {
        QuestGiver[] newFriends = Object.FindObjectsByType<QuestGiver>(FindObjectsSortMode.None); ;
        friendships = new FriendshipData[newFriends.Length];

        for (int i = 0; i < newFriends.Length; i++)
        {
            friendships[i] = newFriends[i].friendshipData;
        }

        for (int i = 0; i < friendships.Length; i++)
        {
            GameObject newFriendshipField = Instantiate(friendShipPrefab, friendShipField.transform);

            TMP_Text nameText = newFriendshipField.GetComponentInChildren<TMP_Text>();
            Slider friendshipSlider = newFriendshipField.GetComponentInChildren<Slider>();

            // Adjust the position of the new UI element
            RectTransform rectTransform = newFriendshipField.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-i * 150, 0);

            friendshipMeters.Add(friendshipSlider);
            friendshipNames.Add(nameText);

            nameText.text = friendships[i].friendName;

            // Set the slider's value based on the friendship value
            friendshipSlider.value = friendships[i].GetFriendValue();
        }
    }

    void SetFriendMeterValue(int friend, int newValue)
    {
        if (friend >= 0 && friend < friendships.Length)
        {
            friendships[friend].friendshipValue = newValue;
            StartCoroutine(SmoothUpdateFriendMeter(friend));
        }
    }

    public void UpdateFriendMeterExternalCall(int friendIndex)
    {
        StartCoroutine(SmoothUpdateFriendMeter(friendIndex));
    }

    void UpdateFriendMeter()
    {
        for (int i = 0; i < friendshipMeters.Count; i++)
        {
            if (friendshipMeters.Count > i)
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
            yield return null;
        }

        friendshipMeters[friend].value = targetValue;
    }
}