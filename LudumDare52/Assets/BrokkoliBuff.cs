using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokkoliBuff : MonoBehaviour
{
    private float buffDistance = 1;
    private int damageBuff;
    private float attackSpeedBuff;
    private List<GameObject> neighboringPlants;

    private PlantStats plantStats;

    private void Start()
    {
        PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats();
        buffDistance = stats.AttackRange;
        attackSpeedBuff = stats.AttackCooldown;
        damageBuff = stats.AttackDamage;
    }
    
    // Update is called once per frame
    void Update()
    {
        GameObject plantSpawner = GameObject.Find("PlantSpawner");
        for (int i = 0; i < plantSpawner.transform.childCount; i++)
        {
            if (Vector3.Distance(plantSpawner.transform.GetChild(i).position, transform.position) <= buffDistance)
            {
                BuffPlant(plantSpawner.transform.GetChild(i).gameObject);
            }
        }
    }

    private void BuffPlant(GameObject obj)
    {
        if (obj.name.Contains("Bell Pepper"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBellPepperStats();
            obj.GetComponent<PaprikaAttack>().SetAttackCooldown(stats.AttackCooldown - attackSpeedBuff);
        }
        if (obj.name.Contains("Brokkoli"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats();
            buffDistance = stats.AttackRange;
        }
        if (obj.name.Contains("Carrot"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBellPepperStats();
            obj.GetComponent<CarrotAttack>().SetAttackCooldown(stats.AttackCooldown - attackSpeedBuff);
        }
        if (obj.name.Contains("Pumpkin"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBellPepperStats();
            obj.GetComponent<PumpkinAttack>().SetAttackDamage(stats.AttackDamage + damageBuff);
        }
    }
}
