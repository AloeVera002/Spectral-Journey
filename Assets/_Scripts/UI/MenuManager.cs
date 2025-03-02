using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject exit;

    public static bool Paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your actual scene name
    }

    public void HowToPlay()
    {
         //Activates the pause menu
        howToPlay.SetActive(true);
        //Freezes time
        Time.timeScale = 0f;
        //Pauses the game
        Paused = true;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
