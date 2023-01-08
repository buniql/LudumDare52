using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStats : MonoBehaviour
{
    public List<PlantAttackStats> Stats;

    public PlantAttackStats GetBellPepperStats()
    {
        PlantAttackStats stats = new PlantAttackStats();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Bell Pepper"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantAttackStats GetBrokkoliStats()
    {
        PlantAttackStats stats = new PlantAttackStats();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Brokkoli"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantAttackStats GetCarrotStats()
    {
        PlantAttackStats stats = new PlantAttackStats();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Carrot"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantAttackStats GetCornStats()
    {
        PlantAttackStats stats = new PlantAttackStats();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Corn"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantAttackStats GetMelonStats()
    {
        PlantAttackStats stats = new PlantAttackStats();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Melon"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantAttackStats GetPumpkinStats()
    {
        PlantAttackStats stats = new PlantAttackStats();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Pumpkin"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    

}

[System.Serializable]
public struct PlantAttackStats
{
    public string Name;
    public float AttackRange;
    public float AttackCooldown;
    public int AttackDamage;
}
