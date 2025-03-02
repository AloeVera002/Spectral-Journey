using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sjSettingsManager : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] Slider audioVolumeSlider;
    [SerializeField] AudioSource settingsMenuSource;

    private static sjSettingsManager settingsInstance;

    [SerializeField] GameObject settingsMenu;

    bool settingsMenuBool = false;
    private bool settingsMusicPlaying = false;

    void Awake()
    {
        if (settingsInstance == null)
        {
            settingsInstance = this;
            DontDestroyOnLoad(gameObject); // Keep this object when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate AudioManager instances
        }
        settingsMenuSource = GetComponent<AudioSource>();

//        SceneManager.sceneLoaded += HandleVolumeChange;
    }

    private void Start()
    {
        HandleVolumeChange();
        settingsMenu = GameObject.Find("SettingsMenu");
        audioVolumeSlider = settingsMenu.transform.GetChild(5).gameObject.GetComponent<Slider>();
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleSettingsMenu();
        }
    }

    public void ToggleSettingsMenu()
    {
        Debug.Log("newBool before: " + settingsMenuBool);

        settingsMenuBool = !settingsMenuBool;
        if (settingsMenuBool)
        {
            ChangeAudioVolume(0);
            if (settingsMusicPlaying)
            {
                settingsMenuSource.UnPause();
            }
            else
            {
                settingsMenuSource.Play();
                settingsMusicPlaying = true;
            }
        }
        else
        {
            ChangeAudioVolume(audioVolumeSlider.value);
            settingsMenuSource.Pause();
        }
        Debug.Log("newBool after: " + settingsMenuBool);
        settingsMenu.SetActive(settingsMenuBool);
    }

    void HandleVolumeChange()//Scene scene, LoadSceneMode mode)
    {
        audioSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }

    void ChangeAudioVolume(float newValue)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source != settingsMenuSource)
            source.volume = newValue;// audioVolumeSlider.value;
        }
    }
}
