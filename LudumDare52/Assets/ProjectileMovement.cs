using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float Speed;
    public float Livetime;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * Speed;
        StartCoroutine(DestoryInTime());
    }
    
    private IEnumerator DestoryInTime()
    {
        yield return new WaitForSeconds(Livetime);
        GameObject.Destroy(this.gameObject);
    }
}
