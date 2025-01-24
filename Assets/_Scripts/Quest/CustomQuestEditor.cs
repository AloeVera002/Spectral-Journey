using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestScriptableObj))]
public class CustomQuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the instance of the ScriptableObject
        QuestScriptableObj rewardConfig = (QuestScriptableObj)target;

        // Draw the isRewardCollectable boolean first
        rewardConfig.isCollectableReward = EditorGUILayout.Toggle("Is Reward Collectable", rewardConfig.isRewardCollectable);

        // Conditionally show the GameObject field if isRewardCollectable is true
        if (rewardConfig.isCollectableReward)
        {
            rewardConfig.rewardObj = (GameObject)EditorGUILayout.ObjectField("Reward Prefab", rewardConfig.rewardPrefab, typeof(GameObject), true);
        }

        // Save any changes made to the object
        serializedObject.ApplyModifiedProperties();
    }
}
