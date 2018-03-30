using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    private List<Vector2Int> tilesPositions = new List<Vector2Int>();


    public void Create(int spawnBoundry, List<Vector2Int> tilesPosition, int nr, int max)
    {
        this.tilesPositions = tilesPosition;
        Vector2Int offset = ChooseMultiSpawnPosition(spawnBoundry, nr, max);

        for (int i = 0; i < tilesPositions.Count; i++)
        {
            Vector2Int position = GetIndex(i) + offset;
            Board.instance.ChooseTileToUse(position);
            Vector2 positionRT = Board.instance.GetBoardTilePosition(position);
            SpawnManager.CreateObject(tilePrefab, positionRT);
        }
    }

    Vector2Int ChooseSpawnPosition(int spawnBoundry)
    {
        Vector2Int size = GetSize();
        int widthMax = Board.instance.Columns - 1 - (size.x + spawnBoundry);
        int heightMax = Board.instance.Rows - 1 - (size.y + spawnBoundry);

        int columnNr = Random.Range(spawnBoundry, widthMax);
        int rowNr = Random.Range(spawnBoundry, heightMax);

        return new Vector2Int(rowNr, columnNr);
    }

    Vector2Int ChooseMultiSpawnPosition(int spawnBoundry, int obstacleNr, int obstacleAmount)
    {
        Vector2Int size = GetSize();
        int widthMin = obstacleNr * Board.instance.Columns / obstacleAmount + spawnBoundry;
        int widthMax = (obstacleNr + 1) * (Board.instance.Columns / obstacleAmount) - (spawnBoundry + size.x);
        int heightMax = Board.instance.Rows - 1 - (size.y + spawnBoundry);

        int columnNr = Random.Range(widthMin, widthMax);
        int rowNr = Random.Range(spawnBoundry, heightMax);

        return new Vector2Int(rowNr, columnNr);
    }

    Vector2Int GetIndex(int nr)
    {
        return tilesPositions[nr];
    }

    public Vector2Int GetSize()
    {
        int height = tilesPositions.Max(t => t.x) + 1;
        int width = tilesPositions.Max(t => t.y) + 1;
        return new Vector2Int(width, height);
    }
}
