using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMAenuAudio : MonoBehaviour
{
    private static MainMAenuAudio instance;
    private AudioSource audioSource;

    public delegate void SwitchScene();
    public event SwitchScene OnSwitchedScene;

    public AudioClip backgroundMusic, cafeSound;

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

        OnSwitchedScene += HandleSwitchScene;
    }

    public void CallSwitchSceneEvent()
    {
        OnSwitchedScene?.Invoke();
    }

    void HandleSwitchScene()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CafeInside"))
        {
            audioSource.clip = cafeSound;
        }
        else 
        {
            audioSource.clip = backgroundMusic;
        }
    }
}
