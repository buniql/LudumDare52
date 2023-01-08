using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[System.Serializable]
public enum InsectEnum
{
    Ladybug, Ant, Beetle, Caterpillar, Spider, Bee, Snail
}

[System.Serializable]
public class Pack
{
    public InsectEnum type;
    public int amount;
    public float timeBetweenSpawn;

    public float TimeNeeded()
    {
        return timeBetweenSpawn*(amount-1);
    }
}

[System.Serializable]
public class Wave
{
    public float timeBetweenPacks = 2f;
    public List<Pack> packs;

    public float TimeNeeded()
    {
        float t = 0f;
        for (int i = 0; i < packs.Count; i++)
            t += packs[i].TimeNeeded();
        return t + (packs.Count - 1) * timeBetweenPacks;
    }
}

public class InsectSpawner : MonoBehaviour
{
    public List<Wave> waves;

    public GameObject ladybugPrefab;
    public GameObject antPrefab;
    public GameObject beetlePrefab;
    public GameObject caterpillarPrefab;
    public GameObject spiderPrefab;
    public GameObject beePrefab;
    public GameObject snailPrefab;

    public Vector3 SpawnPosition;

    public float timeBetweenWaves = 15f;

    private void Start()
    {
        StartCoroutine(SpawnEverything());
    }

    private IEnumerator SpawnEverything()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            StartCoroutine(SpawnWave(waves[i]));
            yield return new WaitForSeconds(timeBetweenWaves + waves[i].TimeNeeded());
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.packs.Count; i++)
        {
            StartCoroutine(SpawnPack(wave.packs[i]));
            yield return new WaitForSeconds(wave.timeBetweenPacks + wave.packs[i].TimeNeeded());
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
            case InsectEnum.Ladybug:
                return ladybugPrefab;
            case InsectEnum.Ant:
                return antPrefab;
            case InsectEnum.Beetle:
                return beetlePrefab;
            case InsectEnum.Caterpillar:
                return caterpillarPrefab;
            case InsectEnum.Spider:
                return spiderPrefab;
            case InsectEnum.Bee:
                return beePrefab;
            case InsectEnum.Snail:
                return snailPrefab;
        }

        // nOt AlL cOdE pAtHs ReTuRn A vAlUe
        return antPrefab;
    }
}
