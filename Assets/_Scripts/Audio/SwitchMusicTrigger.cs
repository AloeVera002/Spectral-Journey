using UnityEngine;
using System.Collections;

public class SwitchMusicTrigger : MonoBehaviour
{

    public AudioClip cafeBGM;
    private AudioManager theAM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

  
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(cafeBGM != null)
            theAM.ChangeBGM(cafeBGM);
        }
    }
}
