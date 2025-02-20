using Unity.Mathematics;
using UnityEngine;

public class AimAssister : MonoBehaviour
{
    [SerializeField] GameObject target;
    private GameObject assister;

    void Start()
    {
        target.GetComponent<Outline>().enabled = false;
        assister = transform.parent.gameObject;
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
        if (!target.GetComponent<Outline>().enabled)
        {
            target.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target.GetComponent<Outline>().enabled = false;
    }

    public void RemoveOutline()
    {
        target.GetComponent<Outline>().enabled = false;
        Invoke("ResetAimTarget", 0.1f);
    }

    public void ResetAimTarget()
    {
        target = null;
    }
}
