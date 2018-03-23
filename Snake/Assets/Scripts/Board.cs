using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab;
    public static int Columns { get; private set; }
    public static int Rows { get; private set; }

    private GridLayoutGroup gridLayout;
    public static List<BoardTile[]> BoardTiles { get; private set; }
    private BoardTileAnimator[] tilesAnimators;

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        CountColumnsAndRows();

        tilesAnimators = new BoardTileAnimator[Rows * Columns];
        BoardTiles = new List<BoardTile[]>();

    }

    void CountColumnsAndRows()
    {
        float spacing = gridLayout.spacing.x;
        Vector2 sizePrefab = tilePrefab.GetComponent<RectTransform>().rect.size;

        Columns = (int)(Screen.width / (sizePrefab.x + spacing));
        Rows = (int)(Screen.height / (sizePrefab.x + spacing));
    }

    public IEnumerator CreateBoard()
    {
        CreateTiles();

        while (true)
        {
            if (tilesAnimators.All(t => t.PlayingAnimationIsDone))
                break;
            yield return null;
        }
    }

    public void CreateTiles()
    {
        gridLayout.constraintCount = Columns;

        for (int r = 0; r < Rows; r++)
        {
            BoardTiles.Add(new BoardTile[Columns]);

            for (int c = 0; c < Columns; c++)
            {
                GameObject go = Instantiate(tilePrefab);
                go.transform.SetParent(transform);
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.localPosition = new Vector3(0, 0, 0);

                BoardTile bt = go.GetComponent<BoardTile>();
                bt.IndexOnBoard = new Vector2Int(r, c);
                BoardTiles[r][c] = bt;
                tilesAnimators[r * Columns + c] = bt.GetComponent<BoardTileAnimator>();
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        gridLayout.enabled = false;
    }

    public static BoardTile GetFirstTilePosition()
    {
        return BoardTiles[0][0];
    }

    public static BoardTile GetBoardTile(Vector2Int index)
    {
        return BoardTiles[index.x][index.y];
    }

    public Vector3 GetRandomFreeTilePosition()
    {
        List<BoardTile> freeTiles = new List<BoardTile>();

        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Columns; c++)
                if (BoardTiles[r][c].IsFree)
                    freeTiles.Add(BoardTiles[r][c]);

        // TODO: if there is one left square win
        return freeTiles.Random().GetPositionRT();
    }
}
