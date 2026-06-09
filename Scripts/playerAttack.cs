using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public float attackRange = 10f;       // Range within which the player can attack
    public float attackDamage = 100f;     // Damage dealt by the player on each attack
    public float attackCooldown = 1f;    // Cooldown between attacks

    private float lastAttackTime;

    void Update()
    {
        // Trigger the attack if cooldown is over
        if (Input.GetButtonDown("Fire1") && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time; // Update the last attack time
        }
    }

    private void Attack()
    {
        // Find all colliders within the attack range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the collider has the "magellanEnemy" tag
            if (hitCollider.CompareTag("magellanEnemy"))
            {
                // Attempt to get the npcFight component on the target
                npcFight targetHealth = hitCollider.GetComponent<npcFight>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(attackDamage);  // Apply damage to enemy
                    Debug.Log("Dealt " + attackDamage + " damage to " + hitCollider.name);
                }
            }
        }
    }
}
