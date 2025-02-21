using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    MainMenu = 0,
    LakeTown = 1,
    ForestTown = 2,
    FarmTown = 3,
    Cafe = 4
}

public class LevelManager : MonoBehaviour
{
    private GameObject targetToTeleport;
    [SerializeField] string id;
    [SerializeField] Vector3 location;
    [SerializeField] SceneEnum sceneToSwitch;
    [SerializeField] bool isPlayerNearby = false;
    [SerializeField] bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            targetToTeleport = other.gameObject;
            isPlayerNearby = true;
            CommenceTeleportation();
        }
    }

    void CommenceTeleportation()
    {
        Debug.Log("CurrentScene: " + SceneManager.GetActiveScene() + "SceneToSwitch: " + SceneManager.GetSceneAt((int)sceneToSwitch));
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneAt((int)sceneToSwitch))
        {
            LoadSceneByEnum(sceneToSwitch);
        }
    }

    public void LoadSceneByEnum(SceneEnum scene)
    {
        SceneManager.LoadScene((int)scene);
        targetToTeleport.transform.position = location;
    }
}
