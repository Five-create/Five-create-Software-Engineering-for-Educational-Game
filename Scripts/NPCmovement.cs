using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCmovement : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    public float walkRadius = 10f; // Radius for random movement

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(RandomWalk());
    }

    IEnumerator RandomWalk()
    {
        while (true)
        {
            // Generate a random point within the radius
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * walkRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, walkRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                animator.SetBool("isWalking", true);
            }
            
            // Wait until NPC reaches the destination
            yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance);
            animator.SetBool("isWalking", false);
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Pause before moving again
        }
    }

}
