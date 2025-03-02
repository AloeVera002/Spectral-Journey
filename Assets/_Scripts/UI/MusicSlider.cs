using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{

    public Slider volumeSlider;
    public AudioSource musicSource;

    void Start()
    {
        // Load saved volume or set default
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSource.volume = volumeSlider.value;

        // Add listener for when the slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // Save volume setting
        PlayerPrefs.Save();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
}
