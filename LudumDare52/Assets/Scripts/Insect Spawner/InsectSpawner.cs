using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class InsectSpawner : MonoBehaviour
{

    public GameObject InsectPrefab;
    public Vector3 SpawnPosition;
    public float SpawnCooldown;
    private float nextSpawnTime;

    public int currentInsectAmount = 1;
    private void Update()
    {
        if (transform.childCount == 0)
        {
            StartCoroutine(SpawnInsects(currentInsectAmount));
            currentInsectAmount++;
        }
    }

    private IEnumerator SpawnInsects(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject insect = GameObject.Instantiate(InsectPrefab, SpawnPosition, Quaternion.identity);
            insect.transform.parent = this.gameObject.transform;
            yield return new WaitForSeconds(SpawnCooldown);
        }
    }
}
