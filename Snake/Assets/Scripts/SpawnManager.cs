using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnObject food;
    [SerializeField] private Board board;

    private RectTransform spawnHolder;

    void Start()
    {
        spawnHolder = GameManager.instance.spawnHolder;
    }

    public void SpawnFood()
    {
        Vector3 pos = board.GetRandomFreeTilePosition();
        CreateObject(food.toSpawn, pos);
    }

    void CreateObject(GameObject prefab, Vector3 pos)
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

    [System.Serializable]
    class SpawnObject
    {
        public GameObject toSpawn;
        public int timeToSpawn;
    }
}
