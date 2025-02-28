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
    }

    //this code is made with the help of chatGPT - Veronika*/

    public Transform centerPoint; // The empty GameObject to act as a center
    public float wanderRadius = 5f; // Maximum distance from the center
    public float moveSpeed = 2f;
    public float waitTime = 2f; // Time to wait before choosing a new position

    private Vector3 targetPosition;
    private bool isWaiting = false;

    void Start()
    {
        ChooseNewTarget();
    }

    void Update()
    {
        if (!isWaiting)
        {
            MoveToTarget();
        }
    }

    void ChooseNewTarget()
    {
        // Pick a random point within the wanderRadius around the center
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        targetPosition = centerPoint.position + new Vector3(randomPoint.x, 0, randomPoint.y);

        // Move to the new position
        isWaiting = false;
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If reached the target, wait before choosing a new target
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            StartCoroutine(WaitBeforeNextMove());
        }
    }

    IEnumerator WaitBeforeNextMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        ChooseNewTarget();
    }
}

