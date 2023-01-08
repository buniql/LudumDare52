using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokkoliBuff : MonoBehaviour
{
    private float buffDistance;
    private int damageBuff;
    private float attackSpeedBuff;
    private List<GameObject> neighboringPlants;

    private void Start()
    {
        PlantAttackStats stats = GameObject.Find("PlantSpawner").GetComponent<PlantStats>().GetBrokkoliStats();
        buffDistance = stats.AttackRange;
        attackSpeedBuff = stats.AttackCooldown;
        attackSpeedBuff = stats.AttackDamage;
    }
    
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject child in GameObject.Find("PlantSpawner").transform)
        {
            if (Vector3.Distance(child.transform.position, transform.position) <= buffDistance)
            {
                
            }
        }
    }

    public void BuffPlant(GameObject obj)
    {
        if (obj.name.Contains("Bell Pepper"))
        {
            //obj.GetComponent<PaprikaAttack>().AttackCooldown ;
        }
        if (obj.name.Contains("Brokkoli"))
        {
            
        }
        if (obj.name.Contains("Carrot"))
        {
            
        }
        if (obj.name.Contains("Melon"))
        {
            
        }
        if (obj.name.Contains("Pumpkin"))
        {
            
        }
    }
}
