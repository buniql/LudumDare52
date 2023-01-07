using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaprikaAttack : MonoBehaviour
{
    public float AttackRange;

    public float AttackCooldown;

    public GameObject WeaponPrefab;

    public int ProjectileAmount;

    private Transform attackTarget;
    private bool currentlyAttacking = false;

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
            if (distance < minDistance && distance <= AttackRange)
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

            for (int i = 0; i < ProjectileAmount; i++)
            {
                Vector2 targetPosition = new Vector2(attackTarget.position.x - transform.position.x,
                    attackTarget.position.y - transform.position.y);
                float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
                angle = 90f + i * (360f / ProjectileAmount);
                GameObject.Instantiate(WeaponPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.Euler(new Vector3(0,0, angle)));
            }

            yield return new WaitForSeconds(AttackCooldown);
            currentlyAttacking = false;
        }
    }
}