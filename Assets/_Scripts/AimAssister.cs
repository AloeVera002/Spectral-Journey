using Unity.Mathematics;
using UnityEngine;

public class AimAssister : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(target.transform);   
        player.transform.LookAt(target.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            target = other.gameObject;
        }
    }
}
