using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] List<TMP_Text> friendshipNamesList = new List<TMP_Text>();
    [SerializeField] List<Slider> friendshipMetersList = new List<Slider>();

    [SerializeField] public FriendshipData[] friendships;
    [SerializeField] GameObject friendShipField;

    [SerializeField] Transform parentTransform;

    [SerializeField] GameObject friendShipPrefab;

    void Start()
    {
        QuestGiver[] newFriends = Object.FindObjectsByType<QuestGiver>(FindObjectsSortMode.None);;
        friendships = new FriendshipData[newFriends.Length];

        for (int i = 0; i < newFriends.Length; i++)
        {
            friendships[i] = newFriends[i].friendshipData;
        }

        for (int i = 0; i < friendships.Length; i++)
        {
            // Instantiate the prefab for each friend
            GameObject newFriendshipField = Instantiate(friendShipPrefab, parentTransform);

            // Get the TMP_Text and Slider components from the instantiated prefab
            TMP_Text nameText = newFriendshipField.GetComponentInChildren<TMP_Text>();
            Slider friendshipSlider = newFriendshipField.GetComponentInChildren<Slider>();

            friendshipMetersList.Add(friendshipSlider);
            friendshipNamesList.Add(nameText);

            // Set the TMP_Text to the friend's name
            nameText.text = friendships[i].friendName;

            // Set the slider's value based on the friendship value
            friendshipSlider.value = friendships[i].GetFriendValue();

            // Adjust the position of the new UI element
            RectTransform rectTransform = newFriendshipField.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -i * 100); // Adjust the Y position for each new item

            // Optionally, you can also store these in your arrays for further use
            // friendshipNames[i] = nameText;
            // friendshipMeters[i] = friendshipSlider;
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