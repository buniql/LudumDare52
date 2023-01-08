using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PumpkinAttack : MonoBehaviour
{
    private int attackDamage;
    private float attackRange;
    private float movementSpeed;
    private Vector3 startPosition;
    private Transform attackTarget;
    private Vector3 targetPosition;
    private bool activated = false;
    private bool homing = false;
    private bool dealDamage = false;
    private Transform jumpTransform;

    // Start is called before the first frame update
    void Start()
    {
        PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetPumpkinStats();
        startPosition = transform.position;

        jumpTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dealDamage = false;

        if (activated)
        {
            if (!homing)
            {
                transform.position +=
                    new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, 0)
                        .normalized * (movementSpeed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position,startPosition) >= Vector3.Distance(targetPosition, startPosition) - .1f)
                {
                    dealDamage = true;
                    homing = true;
                }
            }
            else
            {
                transform.position +=
                    new Vector3(startPosition.x - transform.position.x, startPosition.y - transform.position.y, 0)
                        .normalized * (movementSpeed * Time.fixedDeltaTime);
                if (Vector3.Distance(targetPosition, transform.position) >= Vector3.Distance(targetPosition, startPosition) - .2f)
                {
                    transform.position = startPosition;
                    activated = false;
                    homing = false;
                }
            }
        }
        attackTarget = FindNearestTarget();
        if (attackTarget != null && !activated)
        {
            activated = true;
            targetPosition = attackTarget.position;
        }

        jumpTransform.position = transform.position;
        float onRoute = Mathf.Min(Vector3.Distance(targetPosition, transform.position), Vector3.Distance(startPosition, transform.position));
        float height = onRoute * 0.5f;
        jumpTransform.Translate(new Vector3(0, height, 0));
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Insect")
        {
            if (dealDamage)
            {
                // get insect HP script and apply damage
                other.GetComponent<InsectHealth>().TakeDamage(attackDamage);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    public void SetAttackDamage(int damage)
    {
        this.attackDamage = damage;
    }

    public int GetAttackDamage()
    {
        return this.attackDamage;
    }
}
