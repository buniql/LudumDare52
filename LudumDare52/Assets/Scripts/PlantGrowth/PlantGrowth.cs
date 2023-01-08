using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantGrowth : MonoBehaviour
{
    public PlantType type;
    public GameObject GrownPlantPrefab;
    public enum PlantType
    {
        BellPepper,
        Brokkoli,
        Carrot,
        Corn,
        Melon,
        Pumpkin
    }

    private float growthTime;

    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("PlantSpawner").GetComponent<PlantPlacement>().Tilemap;
        StartCoroutine(
            GrowPlant(GetGrowthTime()));
    }

    private float GetGrowthTime()
    {
        float growthTime;
        switch (type)
        {
            case PlantType.BellPepper:
                growthTime = 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetPumpkinStats().GrowthTime;
                break;
            case PlantType.Brokkoli:
                growthTime = 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats().GrowthTime;
                break;
            case PlantType.Carrot:
                growthTime = 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetCarrotStats().GrowthTime;
                break;
            case PlantType.Corn:
                growthTime = 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetCornStats().GrowthTime;
                break;
            case PlantType.Melon:
                growthTime = 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetMelonStats().GrowthTime;
                break;
            default:
                growthTime = 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetPumpkinStats().GrowthTime;
                break;
        }

        Vector3Int positionToCheck = new Vector3Int((int)Mathf.Floor(transform.position.x),
            (int)Mathf.Floor(transform.position.y), 0);  
        
        if (tilemap.GetTile(positionToCheck).ToString().Contains("watered"))
        {
            growthTime = growthTime * .5f;
        }

        return growthTime;
    }

    private IEnumerator GrowPlant(float growthTime)
    {
        yield return new WaitForSeconds(growthTime);
        GameObject currentPlant = GameObject.Instantiate(GrownPlantPrefab, transform.position, Quaternion.identity);
        currentPlant.transform.parent = GameObject.Find("PlantSpawner").transform;

        GameObject.Destroy(this.gameObject);
    }
}
