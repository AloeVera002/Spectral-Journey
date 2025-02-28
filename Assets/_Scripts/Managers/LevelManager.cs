using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum SceneEnum
{
    MainMenu = 0,
    LakeTown = 1,
    ZombieTestScene = 2,
    FarmTown = 3,
    Cafe = 4
}

public class LevelManager : MonoBehaviour
{
    private GameObject targetToTeleport;
    [SerializeField] Vector3 location;
    [SerializeField] SceneEnum sceneToSwitch;
    AudioManager backgroundAudioMaster;

    private void Start()
    {
        backgroundAudioMaster = GameObject.Find("BackgroundAudioMaster").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetToTeleport = other.gameObject;
 //           targetToTeleport.transform.position = this.transform.position + location;
            SceneManager.LoadScene((int)sceneToSwitch);
        }
    }
}
