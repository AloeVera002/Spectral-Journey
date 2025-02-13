using Unity.Mathematics;
using UnityEngine;

public class AimAssister : MonoBehaviour
{
    [SerializeField] GameObject target;
    private GameObject assister;

    void Start()
    {
        assister = transform.parent.gameObject;
    }

    void Update()
    {
        assister.transform.LookAt(target.transform);
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
