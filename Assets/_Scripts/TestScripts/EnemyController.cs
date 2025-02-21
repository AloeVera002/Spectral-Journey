using System.Xml.Serialization;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent zombie;

    private Outline outline;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = TestPlayerManager.instance.player.transform;
        zombie = GetComponent<NavMeshAgent>();

        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius){
            zombie.SetDestination(target.position);

            if (distance <= zombie.stoppingDistance){
                // Attack the target
                // Face the target
                FaceTarget();
            }
        }
       
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}