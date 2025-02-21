using UnityEngine;

public class TurnBackWarning : MonoBehaviour
{
    [SerializeField] GameObject nothingThisWay;


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
        if (other.gameObject.CompareTag("NothingThisWay"))
        {
            nothingThisWay.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NothingThisWay"))
        {
            nothingThisWay.SetActive(false);
        }
    }
}
