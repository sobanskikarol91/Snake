using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    private List<Vector2Int> tilesPositions = new List<Vector2Int>();


    public void Create(int spawnBoundry, List<Vector2Int> tilesPosition)
    {
        this.tilesPositions = tilesPosition;
        Vector2Int offset = ChooseSpawnPosition(spawnBoundry);

        for (int i = 0; i < tilesPositions.Count; i++)
        {
            Vector2Int position = GetIndex(i) + offset;
            Board.ChooseTileToUse(position);
            Vector2 positionRT = Board.GetBoardTilePosition(position);
            SpawnManager.CreateObject(tilePrefab, positionRT);
        }
    }

    Vector2Int ChooseSpawnPosition(int spawnBoundry)
    {
        Vector2Int size = GetSize();
        int widthMax = Board.Columns - 1 - (size.x + spawnBoundry);
        int heightMax = Board.Rows - 1 - (size.y + spawnBoundry);

        int columnNr = Random.Range(spawnBoundry, widthMax);
        int rowNr = Random.Range(spawnBoundry, heightMax);

        return new Vector2Int(rowNr, columnNr);
    }

    Vector2Int GetIndex(int nr)
    {
        return tilesPositions[nr];
    }

    public Vector2Int GetSize()
    {
        int width = tilesPositions.Max(t => t.x) + 1;
        int height = tilesPositions.Max(t => t.y) + 1;
        return new Vector2Int(width, height);
    }
}
