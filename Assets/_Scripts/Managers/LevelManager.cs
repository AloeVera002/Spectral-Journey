using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    MainMAenuAudio backgroundAudio;

    private void Start()
    {
        backgroundAudio = GameObject.Find("Audio Source").GetComponent<MainMAenuAudio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetToTeleport = other.gameObject;
 //           targetToTeleport.transform.position = this.transform.position + location;
            backgroundAudio.CallSwitchSceneEvent();
            SceneManager.LoadScene((int)sceneToSwitch);
        }
    }
}
