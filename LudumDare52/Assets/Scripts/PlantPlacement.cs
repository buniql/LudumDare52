using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;

public class PlantPlacement : MonoBehaviour
{
    public List<GameObject> PlantPrefabs;

    public Tilemap Tilemap;

    public Camera Camera;
    
    public Vector2 MouseOffset;

    
    public int CurrentPlantPrefabIndex = 0;

    public GameObject CoinsEarnedPrefab;
    

    // Update is called once per frame
    void Update()
    {
        getCurrentPlantKey();

        if (Input.GetMouseButtonDown(0) && CanAffordPlant())
        {
            Vector3 worldPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3 positionToSpawn = new Vector3(Mathf.Ceil(worldPosition.x) - MouseOffset.x,
                Mathf.Ceil(worldPosition.y) - MouseOffset.y, -1);

            bool canSpawn = true;
            
            foreach (Transform child in transform)
            {
                if (child.position == positionToSpawn)
                {
                    canSpawn = false;
                    break;
                }
            }

            Vector3Int positionToCheck = new Vector3Int((int)Mathf.Floor(worldPosition.x),
                (int)Mathf.Floor(worldPosition.y), 0);            
            
            if (canSpawn && Tilemap.GetTile(positionToCheck).ToString().Contains("Field"))
            {
                GameObject currentPlant = GameObject.Instantiate(PlantPrefabs[CurrentPlantPrefabIndex],
                    positionToSpawn, Quaternion.identity);

                currentPlant.transform.parent = this.gameObject.transform;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameObject plantSpawner = GameObject.Find("PlantSpawner");
            for (int i = 0; i < plantSpawner.transform.childCount; i++)
            {
                Vector3 worldPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            
                Vector3 positionToDestory = new Vector3(Mathf.Ceil(worldPosition.x) - MouseOffset.x,
                    Mathf.Ceil(worldPosition.y) - MouseOffset.y, -1);
                
                if (Vector3.Distance(plantSpawner.transform.GetChild(i).position, positionToDestory) <= .6f)
                {
                    SellCurrentPlant(plantSpawner.transform.GetChild(i).name, worldPosition);
                    Destroy(plantSpawner.transform.GetChild(i).gameObject);
                }
            }
        }
    }

    public bool CanAffordPlant()
    {
        PlantStat currentPlantStat = transform.gameObject.GetComponent<PlantStats>().Stats[CurrentPlantPrefabIndex];

        if (GameObject.Find("GameState").GetComponent<GameState>().GetCoins() >= currentPlantStat.BuyPrice)
        {
            GameObject.Find("GameState").GetComponent<GameState>()
                .SetCoins(GameObject.Find("GameState").GetComponent<GameState>().GetCoins() - currentPlantStat.BuyPrice);
            return true;
        }

        return false;
    }

    public void SellCurrentPlant(String name, Vector3 position)
    {
        int amount = 0;

        List<PlantStat> allPlantStats = transform.gameObject.GetComponent<PlantStats>().Stats;
        
        switch (name)
        {
            case "Carrot(Clone)":
                amount = allPlantStats[0].SellPrice;
                break;
            case "Bell Pepper(Clone)":
                amount = allPlantStats[1].SellPrice;
                break;
            case "Corn(Clone)":
                amount = allPlantStats[2].SellPrice;
                break;
            case "Pumpkin(Clone)":
                amount = allPlantStats[3].SellPrice;
                break;
            case "Brokkoli(Clone)":
                amount = allPlantStats[4].SellPrice;
                break;
            case "Melon(Clone)":
                amount = allPlantStats[5].SellPrice;
                break;
        }

        GameObject coin = GameObject.Instantiate(CoinsEarnedPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity);
        coin.GetComponent<CoinsEarned>().Amount = amount;
        
        GameObject.Find("GameState").GetComponent<GameState>()
            .SetCoins(GameObject.Find("GameState").GetComponent<GameState>().GetCoins() + amount);
    }

    private void getCurrentPlantKey()
    {
        if (Input.GetKey("1"))
        {
            CurrentPlantPrefabIndex = 0;
        }
        if (Input.GetKey("2"))
        {
            CurrentPlantPrefabIndex = 1;
        }
        if (Input.GetKey("3"))
        {
            CurrentPlantPrefabIndex = 2;
        }
        if (Input.GetKey("4"))
        {
            CurrentPlantPrefabIndex = 3;
        }
        if (Input.GetKey("5"))
        {
            CurrentPlantPrefabIndex = 4;
        }
        if (Input.GetKey("6"))
        {
            CurrentPlantPrefabIndex = 5;
        }
    }
}