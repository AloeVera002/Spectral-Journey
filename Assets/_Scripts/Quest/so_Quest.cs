using UnityEngine;



[CreateAssetMenu(fileName = "so_Quest", menuName = "Scriptable Objects/so_Quest")]
public class so_Quest : ScriptableObject
{
    [Header("General")]
    public string questName;
    public int questID;
    public string questDetails;

    [Header("Stage")]
    public GameObject[] step;

    [Header("Rewards")]
    public int ectoReward;
    public int friendshipReward;
}
