using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotAttack : MonoBehaviour
{
    public float AttackRange;

    public float AttackCooldown;

    public GameObject WeaponPrefab;

    private Transform attackTarget;
    private bool currentlyAttacking = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        attackTarget = FindNearestTarget();
        if (!currentlyAttacking)
        {
            StartCoroutine(Attack());
            Debug.Log("new Attack");
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
                Debug.Log("new Target");
            }
        }

        
        return result;
    }
    
    private IEnumerator Attack()
    {
        if (attackTarget != null)
        {
            Debug.Log("SHOOT!");
            currentlyAttacking = true;
            GameObject projectile = GameObject.Instantiate(WeaponPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            Debug.Log("Instantiating Projectile");
            Vector2 targetPosition = new Vector2(attackTarget.position.x - transform.position.x,
                attackTarget.position.y - transform.position.y);
            float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle - 90f));
        
            yield return new WaitForSeconds(AttackCooldown);
            currentlyAttacking = false;
        }
    }
}
