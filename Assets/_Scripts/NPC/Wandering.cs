using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour
{
  /* public float wanderRadius = 10f; // The range within which the character will wander
    public float wanderTimer = 5f; // Time before choosing a new destination

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);
        return navHit.position;
    }*/

     public Transform centerPoint; // Empty GameObject as the wander center
    public float wanderRadius = 5f; // Maximum wander distance
    public float waitTime = 2f; // Time before picking a new destination
    public float minDistance = 0.5f; // Distance considered as "arrived"

    private NavMeshAgent agent;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChooseNewTarget();
    }

    void Update()
    {
        if (!isWaiting && agent.remainingDistance <= minDistance)
        {
            StartCoroutine(WaitBeforeNextMove());
        }
    }

    void ChooseNewTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        Vector3 targetPosition = centerPoint.position + new Vector3(randomPoint.x, 0, randomPoint.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            ChooseNewTarget(); // Try again if no valid point was found
        }
    }

    IEnumerator WaitBeforeNextMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        ChooseNewTarget();
    }

    //this code is made with the help of chatGPT - Veronika
}
