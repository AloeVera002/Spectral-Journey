using System.Collections;
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
    [SerializeField] string targetSceneName;
    [SerializeField] Vector3 location;
    [SerializeField] SceneEnum sceneToSwitch;
    [SerializeField] bool isPlayerNearby = false;
    [SerializeField] bool isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            isPlayerNearby = true;
            targetToTeleport = other.gameObject;
           
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneAt((int)sceneToSwitch))
            {
                LoadSceneByEnum(sceneToSwitch);
            }
                
            /*
            StartCoroutine(TeleportPlayer(targetToTeleport));
            CommenceTeleportation();*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            isPlayerNearby = false;
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
    {/*
        SceneManager.LoadScene((int)scene);
        targetToTeleport.transform.position = location;*/

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)sceneToSwitch, LoadSceneMode.Additive);

        asyncLoad.completed += (AsyncOperation op) => OnSceneLoaded((int)sceneToSwitch);
    }

    private void OnSceneLoaded(int sceneIndex)
    {
        if (targetToTeleport != null && SceneManager.GetActiveScene() == SceneManager.GetSceneAt((int)sceneToSwitch))
        {
            // Check for teleportation target using the ID
            LevelManager[] teleportTriggers = GameObject.FindObjectsOfType<LevelManager>();

            foreach (var trigger in teleportTriggers)
            {
                if (trigger.id == id) // Match the ID
                {
                    // Teleport player to the right position
                    targetToTeleport.transform.position = trigger.location;

                    // Optionally add protection from triggering teleportation immediately
                    StartCoroutine(SpawnProtection());
                    break;
                }
            }
        }
    }

    private IEnumerator SpawnProtection()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(0.5f); // Adjust as necessary for protection
        isTeleporting = false;
    }

    private IEnumerator TeleportPlayer(GameObject target)
    {
        isTeleporting = true;

        // Optionally: Add protection to prevent the player from being teleported too close to the trigger again
        yield return new WaitForSeconds(0.5f); // Adjust as necessary for protection

        // Load the target scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)sceneToSwitch, LoadSceneMode.Additive);

        // Wait for the scene to be loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Teleport the player to the new scene's target position
   /*     Scene targetScene = SceneManager.GetSceneByName(targetSceneName);
        GameObject[] teleportTargets = GameObject.FindGameObjectsWithTag("TeleportTarget");

        foreach (GameObject targetc in teleportTargets)
        {
            LevelManager teleportScript = targetc.GetComponent<LevelManager>();

            if (teleportScript != null && teleportScript.teleportTargetID == id)
            {
                // Set the player's position
                player.transform.position = teleportScript.targetPosition;
                break;
            }
        }*/

        // Unload the previous scene if you don't want it loaded permanently
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        isTeleporting = false;
    }
}
