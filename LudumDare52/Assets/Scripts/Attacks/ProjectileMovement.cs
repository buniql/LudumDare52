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

    public enum AttackType
    {
        BellPepper,
        Carrot,
        Pumpkin,
        Melon
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * Speed;
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
}
