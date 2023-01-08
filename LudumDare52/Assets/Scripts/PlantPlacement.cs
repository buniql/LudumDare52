using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;

public class PlantPlacement : MonoBehaviour
{
    public List<GameObject> PlantPrefabs;

    public Tilemap Tilemap;

    public Camera Camera;
    
    public Vector2 MouseOffset;

    private int currentPlantPrefabIndex = 0;
    

    // Update is called once per frame
    void Update()
    {
        getCurrentPlantKey();

        if (Input.GetMouseButtonDown(0))
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
                GameObject currentPlant = GameObject.Instantiate(PlantPrefabs[currentPlantPrefabIndex],
                    positionToSpawn, Quaternion.identity);

                currentPlant.transform.parent = this.gameObject.transform;
            }
        }
    }

    private void getCurrentPlantKey()
    {
        if (Input.GetKey("1"))
        {
            currentPlantPrefabIndex = 0;
        }
        if (Input.GetKey("2"))
        {
            currentPlantPrefabIndex = 1;
        }
        if (Input.GetKey("3"))
        {
            currentPlantPrefabIndex = 2;
        }
        if (Input.GetKey("4"))
        {
            currentPlantPrefabIndex = 3;
        }
        if (Input.GetKey("5"))
        {
            currentPlantPrefabIndex = 4;
        }
        if (Input.GetKey("6"))
        {
            currentPlantPrefabIndex = 5;
        }
    }
}