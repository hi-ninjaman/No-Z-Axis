using UnityEngine;
using UnityEngine.AI;

public class Tracking_Enemy : MonoBehaviour
{
    public Transform[] points;
    public float speed = 1f;
    public float detectionRadius;
    public Transform detectionPoint;
    private int i;
    public Transform target;
    public Transform sweepTarget;
    private NavMeshAgent agent;
    public LayerMask playerLayer;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    public enum State
    {
        Sweeping,
        Pursuit
    }

    public State currentState = State.Sweeping;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        
        /*
        if (target == null || !agent.isOnNavMesh)
        {
            return;
        }
        */
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            i = 1;
            agent.SetDestination(points[i].position);
        }
        else
        {
            Debug.LogError("No NavMesh found near the first waypoint");
            return;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D sensed = Physics2D.OverlapCircle(detectionPoint.transform.position, detectionRadius, playerLayer);

        
        if (sensed != null)
        {
            if (sensed.CompareTag("Player"))
            {
                agent.SetDestination(target.position);
                currentState = State.Pursuit;
            }
        }
        else
        {
            currentState = State.Sweeping;
        }            
        switch (currentState)
        {
            case State.Sweeping:
            agent.isStopped = false;
            if (!agent.pathPending && agent.remainingDistance < 0.2f)
            {
                i = (i + 1) % points.Length;
                agent.SetDestination(points[i].position);
            }
            break;

            case State.Pursuit:
            agent.isStopped = false;
            agent.SetDestination(target.position);
            break;
        }
    }

    void LateUpdate()
    {
    Vector3 pos = transform.position;
    pos.z = transform.position.z;
    transform.position = pos;
    }

}

