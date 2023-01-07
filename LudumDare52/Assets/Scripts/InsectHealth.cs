using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectHealth : MonoBehaviour
{
    public int Health;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(int points)
    {
        Health -= points;

        if (Health < 0)
            GameObject.Destroy(this.gameObject);
    }
}
