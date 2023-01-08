using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokkoliBuff : MonoBehaviour
{
    private float buffDistance = 1;
    private int damageBuff;
    private float attackSpeedBuff;
    private List<GameObject> neighboringPlants;
    private int plantCount;

    private PlantStats plantStats;

    private void Start()
    {
        PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats();
        buffDistance = stats.AttackRange;
        attackSpeedBuff = stats.AttackCooldown;
        damageBuff = stats.AttackDamage;

        GameObject plantSpawner = GameObject.Find("PlantSpawner");
        plantCount = plantSpawner.transform.childCount;
        BuffPlants();
    }
    
    // Update is called once per frame
    void Update()
    {
        GameObject plantSpawner = GameObject.Find("PlantSpawner");
        if (plantCount != plantSpawner.transform.childCount)
        {
            ForceUpdate();
        }
    }

    public void ForceUpdate()
    {
        UnBuffPlants();
        BuffPlants();
    }

    private void BuffPlants()
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
    
    private void UnBuffPlants()
    {
        GameObject plantSpawner = GameObject.Find("PlantSpawner");
        for (int i = 0; i < plantSpawner.transform.childCount; i++)
        {
            if (Vector3.Distance(plantSpawner.transform.GetChild(i).position, transform.position) <= buffDistance)
            {
                UnBuffPlant(plantSpawner.transform.GetChild(i).gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        UnBuffPlants();
    }

    private void BuffPlant(GameObject obj)
    {
        if (obj.name.Equals("Bell Pepper(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBellPepperStats();
            obj.GetComponent<PaprikaAttack>().SetAttackCooldown(obj.GetComponent<PaprikaAttack>().GetAttackCooldown() - attackSpeedBuff);
        }
        /**
        if (obj.name.Equals("Brokkoli(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats();
            obj.GetComponent<BrokkoliBuff>().SetBuffDistance(obj.GetComponent<BrokkoliBuff>().GetBuffDistance() + stats.AttackRange);
        }
        */
        if (obj.name.Equals("Carrot(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetCarrotStats();
            obj.GetComponent<CarrotAttack>().SetAttackCooldown(obj.GetComponent<CarrotAttack>().GetAttackCooldown() - attackSpeedBuff);
        }
        if (obj.name.Equals("Pumpkin(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetPumpkinStats();
            obj.GetComponent<PumpkinAttack>().SetAttackDamage(obj.GetComponent<PumpkinAttack>().GetAttackDamage() + damageBuff);
        }
    }
    
    private void UnBuffPlant(GameObject obj)
    {
        if (obj.name.Equals("Bell Pepper(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBellPepperStats();
            obj.GetComponent<PaprikaAttack>().SetAttackCooldown(obj.GetComponent<PaprikaAttack>().GetAttackCooldown() + attackSpeedBuff);
        }
        /**
        if (obj.name.Equals("Brokkoli(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats();
            obj.GetComponent<BrokkoliBuff>().SetBuffDistance(obj.GetComponent<BrokkoliBuff>().GetBuffDistance() - stats.AttackRange);
        }
        */
        if (obj.name.Equals("Carrot(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetCarrotStats();
            obj.GetComponent<CarrotAttack>().SetAttackCooldown(obj.GetComponent<CarrotAttack>().GetAttackCooldown() + attackSpeedBuff);
        }
        if (obj.name.Equals("Pumpkin(Clone)"))
        {
            PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetPumpkinStats();
            obj.GetComponent<PumpkinAttack>().SetAttackDamage(obj.GetComponent<PumpkinAttack>().GetAttackDamage() - damageBuff);
        }
    }

    public void SetBuffDistance(float distance)
    {
        this.buffDistance = distance;
    }
    
    public float GetBuffDistance()
    {
        return this.buffDistance;
    }
}
