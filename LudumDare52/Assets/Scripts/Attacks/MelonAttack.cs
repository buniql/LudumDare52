using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MelonAttack : MonoBehaviour
{
    public float AttackRange;
    public float MovementSpeed;
    public float RotationSpeed;
    public float Livetime;
    
    private bool activated = false;
    private Transform attackTarget;
    private Vector3 direction;

    private void Start()
    {
        PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetMelonStats();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activated)
        {
            transform.position += direction * (MovementSpeed * Time.fixedDeltaTime);
            if (direction.x > 0)
            {
                transform.Rotate(-Vector3.forward * (RotationSpeed * Time.fixedDeltaTime));
            }
            else
            {
                transform.Rotate(Vector3.forward * (RotationSpeed * Time.fixedDeltaTime));
            }
            StartCoroutine(DestoryInTime());
        }
        else
        {
            attackTarget = FindNearestTarget();
        }
        
        if (attackTarget != null && !activated)
        {
            activated = true;

            direction = new Vector3(attackTarget.position.x - transform.position.x,
                attackTarget.position.y - transform.position.y, 0).normalized;
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
    
    private IEnumerator DestoryInTime()
    {
        yield return new WaitForSeconds(Livetime);
        GameObject.Destroy(this.gameObject);
    }
}
