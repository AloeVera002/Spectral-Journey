using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // Set the scene name in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            Debug.Log("hui");
            SceneManager.LoadScene(sceneToLoad); // Load the new scene

        }
    }
}
