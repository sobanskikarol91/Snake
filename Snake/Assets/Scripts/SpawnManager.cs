﻿using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Header("Test Settings")]
    public bool spawnObstacles = true;

    [Header("Game Settings")]
    [SerializeField] private GameObject foodPrefab;
    public static RectTransform spawnHolder;

    private ObstacleSpawner obstacleSpawner;


    void Start()
    {
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        spawnHolder = GameManager.instance.spawnHolder;
    }

    public void Spawn()
    {
        if (spawnObstacles) SpawnObstacle();
        SpawnFood();
    }

    public void SpawnFood()
    {
        Vector3 pos = Board.instance.GetRandomFreeTilePosition();
        CreateObject(foodPrefab, pos);
    }

    void SpawnObstacle()
    {
        obstacleSpawner.SpawnRandom();
    }

    public static void CreateObject(GameObject prefab, Vector3 pos)
    {
        // Create new snake Tile
        RectTransform newTileRT = Instantiate(prefab).GetComponent<RectTransform>();
        // Set parent to Snake holder
        newTileRT.transform.SetParent(spawnHolder);
        // Change scale to 1
        newTileRT.transform.localScale = new Vector3(1, 1, 1);
        // Set RT world position relative to last snake tile - head
        newTileRT.position = pos;
    }

    public void Restart()
    {
        obstacleSpawner.Restart();
        Spawn();
    }
}
