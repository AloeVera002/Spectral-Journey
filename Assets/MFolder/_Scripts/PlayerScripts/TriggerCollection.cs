using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerCollection : MonoBehaviour
{

    [Header("NPCInteraction")]
    [SerializeField] GameObject interactKey;

    [SerializeField] GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            interactKey.SetActive(true);
        }

        if (other.gameObject.CompareTag("Zombie")){
            Destroy(player);
            SceneManager.LoadScene("ZombieTestScene");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            interactKey.SetActive(false);
        }
    }
}
