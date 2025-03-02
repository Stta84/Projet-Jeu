using UnityEngine;
using UnityEngine.AI; 

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.ResetPath(); 
    }

    void Update()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.Log("Chemin invalide, recalcul...");
            agent.ResetPath();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (agent.pathStatus != NavMeshPathStatus.PathInvalid) 
                {
                    agent.SetDestination(hit.point);
                }
            }
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) 
        {
            Debug.Log("Obstacle détecté, recalcul du chemin !");
            agent.ResetPath();
        }
    }

}