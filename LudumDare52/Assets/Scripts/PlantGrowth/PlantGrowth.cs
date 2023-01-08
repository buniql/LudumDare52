using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(
            GrowPlant(GetGrowthTime()));
    }

    private float GetGrowthTime()
    {
        switch (type)
        {
            case PlantType.BellPepper:
                return 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetPumpkinStats().GrowthTime;
            case PlantType.Brokkoli:
                return 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats().GrowthTime;
            case PlantType.Carrot:
                return 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetCarrotStats().GrowthTime;
            case PlantType.Corn:
                return 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetCornStats().GrowthTime;
            case PlantType.Melon:
                return 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetMelonStats().GrowthTime;
            default:
                return 
                    GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetPumpkinStats().GrowthTime;
        }
    }

    private IEnumerator GrowPlant(float growthTime)
    {
        yield return new WaitForSeconds(growthTime);
        GameObject currentPlant = GameObject.Instantiate(GrownPlantPrefab, transform.position, Quaternion.identity);
        currentPlant.transform.parent = GameObject.Find("PlantSpawner").transform;
        GameObject.Destroy(this.gameObject);
    }
}
