using Unity.Mathematics;
using UnityEngine;

public class AimAssister : MonoBehaviour
{
    [SerializeField] GameObject target;

    void Start()
    {
        
    }

    void Update()
    {
        transform.parent.gameObject.transform.LookAt(target.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetAimTarget();
    }

    public void ResetAimTarget()
    {
        target = null;
    }
}
