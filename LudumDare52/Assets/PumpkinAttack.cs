using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PumpkinAttack : MonoBehaviour
{
    public float AttackRange;
    public float MovementSpeed;
    private Vector3 startPosition;
    private Transform attackTarget;
    private Vector3 targetPosition;
    private bool activated = false;
    private bool homing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activated)
        {
            if (!homing)
            {
                transform.position +=
                    new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, 0)
                        .normalized * (MovementSpeed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position,startPosition) >= Vector3.Distance(targetPosition, startPosition))
                {
                    Debug.Log("I wanna go home now");
                    homing = true;
                }
            }
            else
            {
                transform.position +=
                    new Vector3(startPosition.x - transform.position.x, startPosition.y - transform.position.y, 0)
                        .normalized * (MovementSpeed * Time.fixedDeltaTime);
                if (Vector3.Distance(targetPosition, transform.position) >= Vector3.Distance(targetPosition, startPosition))
                {
                    Debug.Log("I arrived at home");
                    transform.position = startPosition;
                    activated = false;
                    homing = false;
                }
            }
        }
        attackTarget = FindNearestTarget();
        if (attackTarget != null && !activated)
        {
            Debug.Log("Im going to kill those pesants");
            activated = true;
            targetPosition = attackTarget.position;
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
}
