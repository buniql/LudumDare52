using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Globalization;

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
    public List<Pack> packs = new List<Pack>();

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
    public TextAsset waveData;

    public List<Wave> waves = new List<Wave>();

    public GameObject ladybugPrefab;
    public GameObject antPrefab;
    public GameObject beetlePrefab;
    public GameObject caterpillarPrefab;
    public GameObject spiderPrefab;
    public GameObject beePrefab;
    public GameObject snailPrefab;

    public Vector3 SpawnPosition;

    public float timeBetweenWaves = 15f;

    public int beginWithWave = 0;

    private void ReadWaveDataFromCSV()
    {
        var csv = waveData;

        var rows = csv.text.Split(new char[] { '\n' });

        waves.Clear();

        for (int i = 0; i < rows.Length; i++)
        {
            string[] wavedata = rows[i].Split(new char[] { ',' });

            Wave wave = new Wave();
            wave.timeBetweenPacks = float.Parse(wavedata[0], CultureInfo.InvariantCulture);

            for (int j = 0; j < 5; j++)
            {
                int dataOffset = 1 + j*3;

                Pack pack = new Pack();
                switch (wavedata[dataOffset])
                {
                    case "Ladybug":     pack.type = InsectEnum.Ladybug; break;
                    case "Ant":         pack.type = InsectEnum.Ant; break;
                    case "Beetle":      pack.type = InsectEnum.Beetle; break;
                    case "Caterpillar": pack.type = InsectEnum.Caterpillar; break;
                    case "Spider":      pack.type = InsectEnum.Spider; break;
                    case "Bee":         pack.type = InsectEnum.Bee; break;
                    case "Snail":       pack.type = InsectEnum.Snail; break;
                    case "-":           continue;
                    default:            Debug.Log("Unknown insect type " + wavedata[dataOffset] + ", skipping"); break;
                }

                pack.amount = int.Parse(wavedata[dataOffset + 1]);
                pack.timeBetweenSpawn = float.Parse(wavedata[dataOffset + 2], CultureInfo.InvariantCulture);

                wave.packs.Add(pack);
            }

            waves.Add(wave);
        }
        /*
        for (int wave = 0; wave < waves.Count; wave++)
        {
            Debug.Log("wave " + wave + " starts with " + waves[wave].packs[0].amount + " " + waves[wave].packs[0].type + " (time between spawns: " + waves[wave].packs[0].timeBetweenSpawn);
        }*/
    }

    public TMP_Text CurrentWaveText;

    private void Start()
    {
        ReadWaveDataFromCSV();
        StartCoroutine(SpawnEverything());
    }

    private IEnumerator SpawnEverything()
    {
        for (int i = beginWithWave; i < waves.Count; i++)
        {
            CurrentWaveText.text = "Wave " + (i + 1);
            StartCoroutine(SpawnWave(waves[i]));
            yield return new WaitForSeconds(timeBetweenWaves + waves[i].TimeNeeded());
        }

        while (GameObject.Find("InsectSpawner").transform.childCount > 0)
        {
        }
        
        GameObject.Find("Sound").GetComponent<Sound>().PlaySound(8); 
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
