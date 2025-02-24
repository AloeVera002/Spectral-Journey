using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic, cafeSound;
    private static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate AudioManager instances
        }
        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += HandleOnSceneLoaded;
    }

    void HandleOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleSwitchScene();
    }

    void HandleSwitchScene()
    {
        Debug.Log("called HandleSwitchScene from MainMAenuAudio");
        if (SceneManager.GetActiveScene().name == "CafeInside") //SceneManager.GetSceneByName("CafeInside"))
        {
            Debug.Log("Scene is CafeInside");
            audioSource.clip = cafeSound;
        }
        else // if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LakeTownPrototype"))
        {
            Debug.Log("Scene is not CafeInside");
            if (audioSource.clip != backgroundMusic)
            {
                audioSource.clip = backgroundMusic;
            }
            else
            {
                return;
            }
        }
        audioSource.Play();
    }
}
