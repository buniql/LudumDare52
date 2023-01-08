using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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
}

[System.Serializable]
public class Wave
{
    public List<Pack> packs;
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
    public float timeBetweenPacks = 2f;

    public TMP_Text CurrentWaveText;

    private void Start()
    {
        StartCoroutine(SpawnEverything());
    }

    private IEnumerator SpawnEverything()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            CurrentWaveText.text = "Wave " + (i + 1);
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
