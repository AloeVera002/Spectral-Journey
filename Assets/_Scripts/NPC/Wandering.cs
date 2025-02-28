using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public enum EWanderingState
{
    waiting,
    wandering
}

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
    [SerializeField] private EWanderingState wanderingState = EWanderingState.wandering;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChooseNewTarget();
    }

    void Update()
    {
        if (wanderingState == EWanderingState.wandering && agent.remainingDistance <= minDistance)
        {
            wanderingState = EWanderingState.waiting;
            Invoke("ChooseNewTarget", 2f);
        }
    }

    void ChooseNewTarget()
    {
        wanderingState = EWanderingState.wandering;

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

    //this code is made with the help of chatGPT and Benjamin doing damage control - Veronika
}
