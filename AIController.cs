using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public float speedWalk = 3.5f;
    public float speedRun = 6f;
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    private int currentWaypointIndex = 0;
    private Transform player;
    private bool isChasing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (waypoints.Length > 0)
        {
            GoToNextWaypoint();
        }
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            isChasing = true;
            ChasePlayer();
        }
        else if (isChasing)
        {
            isChasing = false;
            GoToNextWaypoint();
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void ChasePlayer()
    {
        navMeshAgent.speed = speedRun;
        navMeshAgent.SetDestination(player.position);
    }

    bool CanSeePlayer()
    {
        Collider[] playersInView = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        foreach (var hit in playersInView)
        {
            Transform target = hit.transform;
            Vector3 dirToPlayer = (target.position - transform.position).normalized;
            float distToPlayer = Vector3.Distance(transform.position, target.position);

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToPlayer, distToPlayer, obstacleMask))
                {
                    return true; 
                }
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
