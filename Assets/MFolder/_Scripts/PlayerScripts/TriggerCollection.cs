using UnityEditor.Search;
using UnityEngine;

public class TriggerCollection : MonoBehaviour
{

    [Header("NPCInteraction")]
    [SerializeField] GameObject interactKey;



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
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            interactKey.SetActive(false);
        }
    }
}
