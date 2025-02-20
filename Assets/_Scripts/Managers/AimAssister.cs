using Unity.Mathematics;
using UnityEngine;

public class AimAssister : MonoBehaviour
{
    [SerializeField] GameObject target;
    private GameObject assister;
 //   private Outline targetOutline;

    void Start()
    {
     /*   targetOutline = GetComponent<Outline>();
        targetOutline.enabled = false;*/

        assister = transform.parent.gameObject;
    }

    void Update()
    {
        if (target)
        {
        //    if (Vector3.Distance(assister.transform.position, target.transform.position) < .5)
        //    {
        //        Debug.Log(Vector3.Distance(assister.transform.position, target.transform.position));
                assister.transform.LookAt(target.transform);
        //        OutlineCheck();
        //    }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            target = other.gameObject;
        }
    }/*

    private void OnTriggerStay(Collider other)
    {
        OutlineCheck();
    }

    void OutlineCheck()
    {
        if (!targetOutline.enabled)
        {
            targetOutline.enabled = true;
        }
        else
        {
            targetOutline.enabled = false;
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        target.GetComponent<Outline>().enabled = false;
    }
    
    public void RemoveOutline()
    {
    //    target.GetComponent<Outline>().enabled = false;
    //    Invoke("ResetAimTarget", 0.1f);
    ResetAimTarget();
    }

    public void ResetAimTarget()
    {
        target = null;
    }
}
