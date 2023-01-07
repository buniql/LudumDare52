using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public AttackType ProjectileAttackType;
    public int AttackDamage;
    public float Speed;
    public float Livetime;
    private CircleCollider2D Collider;

    public enum AttackType
    {
        BellPepper,
        Carrot,
        Pumpkin,
        Melon
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * Speed * Time.fixedDeltaTime;
        StartCoroutine(DestoryInTime());
    }

    private void CarrotProjectile()
    {
        transform.position += transform.up * Speed;
    }
    private IEnumerator DestoryInTime()
    {
        yield return new WaitForSeconds(Livetime);
        GameObject.Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Insect")
        {
            // get insect HP script and apply damage
            other.GetComponent<InsectHealth>().TakeDamage(AttackDamage);

            GameObject.Destroy(this.gameObject);
        }
    }
}
