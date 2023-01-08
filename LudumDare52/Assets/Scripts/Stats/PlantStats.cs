using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantStats : MonoBehaviour
{
    public List<PlantStat> Stats;

    public List<TMP_Text> CostTexts;

    private void Awake()
    {
        CostTexts[0].text = GetCarrotStats().BuyPrice.ToString();
        CostTexts[1].text = GetBellPepperStats().BuyPrice.ToString();
        CostTexts[2].text = GetCornStats().BuyPrice.ToString();
        CostTexts[3].text = GetPumpkinStats().BuyPrice.ToString();
        CostTexts[4].text = GetBrokkoliStats().BuyPrice.ToString();
        CostTexts[5].text = GetMelonStats().BuyPrice.ToString();
    }

    public PlantStat GetBellPepperStats()
    {
        PlantStat stats = new PlantStat();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Bell Pepper"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantStat GetBrokkoliStats()
    {
        PlantStat stats = new PlantStat();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Brokkoli"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantStat GetCarrotStats()
    {
        PlantStat stats = new PlantStat();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Carrot"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantStat GetCornStats()
    {
        PlantStat stats = new PlantStat();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Corn"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantStat GetMelonStats()
    {
        PlantStat stats = new PlantStat();
        foreach (var stat in Stats)
        {
            if (stat.Name.Contains("Melon"))
            {
                stats = stat;
            }
        }

        return stats;
    }
    
    public PlantStat GetPumpkinStats()
    {
        PlantStat stats = new PlantStat();
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
public struct PlantStat
{
    public string Name;
    public float AttackRange;
    public float AttackCooldown;
    public int AttackDamage;
    public float GrowthTime;
    public int BuyPrice;
    public int SellPrice;
}
