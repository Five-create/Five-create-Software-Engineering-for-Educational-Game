using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcAttackMagellan : MonoBehaviour
{
    public Transform target;
    public float attackRange = 2f;       // The distance within which to attack
    public float attackDamage = 100f;     // How much damage is dealt on attack
    public float attackCooldown = 0.1f;    // Cooldown between attacks
    private NavMeshAgent agent;
    private float lastAttackTime;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target != null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("magellanEnemy");
            if (enemies.Length > 0)
            {
                target = enemies[0].transform;  // Choose the first enemy as the target
            }
        }
    }
    void Update()
    {
        if (target != null)
        {
            // Move toward the target (enemy NPC)
            agent.SetDestination(target.position);

            // Check the distance to the target
            float distance = Vector3.Distance(transform.position, target.position);

            // If close enough to attack and the cooldown is over, attack
            if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        // Find all colliders within the attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the collider has the "lapulapuEnemy" tag
            if (hitCollider.CompareTag("magellanEnemy"))
            {
                // Attempt to get the npcFight component on the target
                npcFight targetHealth = hitCollider.GetComponent<npcFight>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(attackDamage);  // Apply damage to the enemy
                }
            }
        }
    }
}
