using UnityEngine;
using UnityEngine.UI;

public class HowToPlayButton : MonoBehaviour
{

    public GameObject screen;
    public GameObject howTo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WhenButtonClicked()
    {
        Debug.Log("Button is pressed");
        screen.SetActive(true);
        
    }

    public void CloseWindow()
    {
        screen.SetActive(false);
    }

    public void CloseHowTo()
    {
        howTo.SetActive(false);
    }
}
