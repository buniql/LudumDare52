using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornAttack : MonoBehaviour
{
    private float attackRange;
    private float attackCooldown;
    private int attackDamage;

    public GameObject WeaponPrefab;

    private Transform attackTarget;
    private bool currentlyAttacking = false;

    private void Start()
    {
        PlantStat stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetCornStats();
        attackRange = stats.AttackRange;
        attackCooldown = stats.AttackCooldown;
        attackDamage = stats.AttackDamage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attackTarget = FindNearestTarget();
        if (!currentlyAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private Transform FindNearestTarget()
    {
        GameObject insectSpawner = GameObject.Find("InsectSpawner");
        Transform result = null;
        float minDistance = Mathf.Infinity;
        foreach (Transform child in insectSpawner.transform)
        {
            float distance = Vector3.Distance(transform.position, child.position);
            if (distance < minDistance && distance <= attackRange)
            {
                result = child;
                minDistance = distance;
            }
        }

        
        return result;
    }
    
    private IEnumerator Attack()
    {
        if (attackTarget != null)
        {
            currentlyAttacking = true;
            
            Vector2 targetPosition = new Vector2(attackTarget.position.x - transform.position.x,
                attackTarget.position.y - transform.position.y);
            float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
                
            GameObject projectile = GameObject.Instantiate(WeaponPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.Euler(new Vector3(0,0, angle - 90f)));
            projectile.GetComponent<ProjectileMovement>().AttackDamage = attackDamage;

            GameObject.Find("Sound").GetComponent<Sound>().PlaySound(2);  
            GameObject.Instantiate(WeaponPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.Euler(new Vector3(0,0, angle - 90f)));
        
            yield return new WaitForSeconds(attackCooldown);
            currentlyAttacking = false;
        }
    }
}
