using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public int spawnBoundry = 1;
    public ObstaclePositions obstaclePosition = new ObstaclePositions();

    [Range(0,2)]
    [SerializeField] private int amountToSpawn = 2;
    public GameObject[] obstaclePrefab;

    public void SpawnRandom()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject randomPrefab = obstaclePrefab.Random();
            GameObject newObstacle = Instantiate(randomPrefab);

            newObstacle.transform.SetParent(SpawnManager.spawnHolder);
            newObstacle.transform.localScale = new Vector3(1, 1, 1);
            List<Vector2Int> tilesPositions = obstaclePosition.GetPositionsList();
            newObstacle.GetComponent<Obstacle>().Create(spawnBoundry, tilesPositions, i, amountToSpawn);
        }
    }

    public void Restart()
    {
        obstaclePosition.Restart();
    }
}
