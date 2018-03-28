using UnityEngine;
using System.Collections.Generic;


public class ObstaclePositions 
{
    private List<List<Vector2Int>> tilesPositions = new List<List<Vector2Int>>();

    public ObstaclePositions()
    {
        CreatePosistionList();
    }

    public void CreatePosistionList()
    {
        Bow();
        Square();
        Cross();
        Strange();
    }

    public List<Vector2Int> GetPositionsList()
    {
        return tilesPositions.ReturnAndRemoveRandom();
    }

    public void Restart()
    {
        tilesPositions = new List<List<Vector2Int>>();
        CreatePosistionList();
    }

    void Strange()
    {
        List<Vector2Int> list = new List<Vector2Int>();

        AddPositionToList(0, 0, list);
        AddPositionToList(0, 1, list);
        AddPositionToList(0, 2, list);
        AddPositionToList(0, 3, list);
        AddPositionToList(0, 4, list);
        AddPositionToList(0, 5, list);
        AddPositionToList(1, 0, list);
        AddPositionToList(1, 5, list);
        AddPositionToList(2, 0, list);
        AddPositionToList(2, 5, list);
        AddPositionToList(3, 0, list);
        AddPositionToList(3, 5, list);
        AddPositionToList(4, 0, list);
        AddPositionToList(4, 1, list);
        AddPositionToList(4, 4, list);
        AddPositionToList(4, 5, list);

        tilesPositions.Add(list);
    }

    void Bow()
    {
        List<Vector2Int> list = new List<Vector2Int>();

        AddPositionToList(0, 0, list);
        AddPositionToList(0, 1, list);
        AddPositionToList(0, 2, list);
        AddPositionToList(0, 3, list);
        AddPositionToList(0, 4, list);
        AddPositionToList(1, 0, list);
        AddPositionToList(1, 4, list);
        AddPositionToList(2, 0, list);
        AddPositionToList(2, 4, list);

        tilesPositions.Add(list);
    }

    void Square()
    {
        List<Vector2Int> list = new List<Vector2Int>();

        AddPositionToList(0, 0, list);
        AddPositionToList(0, 1, list);
        AddPositionToList(1, 0, list);
        AddPositionToList(1, 1, list);

        tilesPositions.Add(list);
    }

    void Cross()
    {
        List<Vector2Int> list = new List<Vector2Int>();

        AddPositionToList(0, 2, list);
        AddPositionToList(1, 2, list);
        AddPositionToList(2, 2, list);
        AddPositionToList(3, 2, list);
        AddPositionToList(4, 2, list);

        AddPositionToList(2, 0, list);
        AddPositionToList(2, 1, list);
        AddPositionToList(2, 3, list);
        AddPositionToList(2, 4, list);

        tilesPositions.Add(list);
    }

    void AddPositionToList(int x, int y, List<Vector2Int> list)
    {
        list.Add(new Vector2Int(x, y));
    }
}
