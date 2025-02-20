using Unity.Mathematics;
using UnityEngine;

public class AimAssister : MonoBehaviour
{
    [SerializeField] GameObject target;
    private GameObject assister;

    void Start()
    {
        assister = transform.parent.gameObject;
        target.GetComponent<Outline>().enabled = false;
    }

    void Update()
    {
        if (target)
        {
            assister.transform.LookAt(target.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OutlineCheck();
    }

    void OutlineCheck()
    {
        if (target.GetComponent<Outline>().enabled)
        {
            target.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetAimTarget();
    }

    public void ResetAimTarget()
    {
        target.GetComponent<Outline>().enabled = false;
        target = null;
    }
}
