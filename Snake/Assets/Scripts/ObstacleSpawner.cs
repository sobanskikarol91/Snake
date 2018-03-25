using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public int spawnBoundry = 1;
    [SerializeField] private int amountToSpawn = 2;

    public GameObject[] obstaclePrefab;
 
    public void SpawnRandom()
    {
        GameObject randomPrefab = obstaclePrefab.Random();
        GameObject newObstacle = Instantiate(randomPrefab);

        newObstacle.transform.SetParent(SpawnManager.spawnHolder);
        newObstacle.transform.localScale = new Vector3(1, 1, 1);
        newObstacle.GetComponent<Obstacle>().Create(spawnBoundry);
    }
}
