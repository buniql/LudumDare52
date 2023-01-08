using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[System.Serializable]
public enum InsectEnum
{
    Ant, Ladybug, Spider, Bee
}

[System.Serializable]
public class Pack
{
    public InsectEnum type;
    public int amount;
    public float timeBetweenSpawn;
}

[System.Serializable]
public class Wave
{
    public List<Pack> packs;
}

public class InsectSpawner : MonoBehaviour
{
    public List<Wave> waves;

    public GameObject antPrefab;
    public GameObject ladybugPrefab;
    public GameObject spiderPrefab;
    public GameObject beePrefab;

    public GameObject InsectPrefab;
    public Vector3 SpawnPosition;
    //public float SpawnCooldown;
    //private float nextSpawnTime;

    public float timeBetweenWaves = 15f;
    public float timeBetweenPacks = 2f;

    private void Start()
    {
        StartCoroutine(SpawnEverything());
    }

    private IEnumerator SpawnEverything()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            StartCoroutine(SpawnWave(waves[i]));
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.packs.Count; i++)
        {
            StartCoroutine(SpawnPack(wave.packs[i]));
            yield return new WaitForSeconds(timeBetweenPacks);
        }
    }

    private IEnumerator SpawnPack(Pack pack)
    {
        GameObject prefab = GetPrefab(pack.type);

        for (int i = 0; i < pack.amount; i++)
        {
            GameObject insect = GameObject.Instantiate(prefab, SpawnPosition, Quaternion.identity);
            insect.transform.parent = this.gameObject.transform;
            yield return new WaitForSeconds(pack.timeBetweenSpawn);
        }
    }

    private GameObject GetPrefab(InsectEnum type)
    {
        switch (type)
        {
            case InsectEnum.Ant:
                return antPrefab;

            case InsectEnum.Ladybug:
                return ladybugPrefab;

            case InsectEnum.Spider:
                return spiderPrefab;

            case InsectEnum.Bee:
                return beePrefab;
        }

        // failsafe
        return antPrefab;
    }

    /*
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
    }*/
}
