using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class randomPatrollingNpc : MonoBehaviour
{
    GameObject player;
    NavMeshAgent zombie;
    [SerializeField] LayerMask groundLayer, playerLayer;

    // Patrol
    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float range;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zombie = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        Patrol();
    }

    void Patrol(){
        if (!walkPointSet){
            SearchForDest();
        }

        if (walkPointSet){
            zombie.SetDestination(destPoint);
        }

        if (Vector3.Distance(transform.position, destPoint) < 10){
            walkPointSet = false;
        }
    }

    void SearchForDest(){
        float Z = Random.Range(-range, range);
        float X = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + X, transform.position.y, transform.position.z + Z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer)){
            walkPointSet = true;
        }
    }
}
