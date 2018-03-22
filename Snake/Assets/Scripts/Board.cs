using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class Board : MonoBehaviour
{
    public GameObject tilePrefab;
    public int Columns { get; private set; }
    public int Rows { get; private set; }

    private GridLayoutGroup gridLayout;
    private List<BoardTile[]> boardTiles;

    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        Columns = Screen.width / 100;
        Rows = Screen.height / 100;
        boardTiles = new List<BoardTile[]>();

        gridLayout = GetComponent<GridLayoutGroup>();
    }

    public void CreateBoard()
    {
        gridLayout.constraintCount = Columns;

        for (int r = 0; r < Rows; r++)
        {
            boardTiles.Add(new BoardTile[Columns]);

            for (int c = 0; c < Columns; c++)
            {
                GameObject go = Instantiate(tilePrefab);
                go.transform.SetParent(transform);
                go.transform.localScale = new Vector3(1, 1, 1);

                BoardTile bt = go.GetComponent<BoardTile>();
                bt.IndexOnBoard = new Vector2Int(r, c);
                bt.row = r;
                bt.col = c;
                boardTiles[r][c] = bt;
               // boardTiles[r][c].name = (r * Columns + c).ToString();
            }
        }
    }

    public BoardTile GetFirstTilePosition()
    {
        return boardTiles[0][0];
    }

    public BoardTile GetBoardTile(Vector2Int index)
    {
        return boardTiles[index.x][index.y];
    }

    public Vector3 GetRandomFreeTilePosition()
    {
        List<BoardTile> freeTiles = new List<BoardTile>();

        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Columns; c++)
                if (boardTiles[r][c].IsFree)
                    freeTiles.Add(boardTiles[r][c]);

        return freeTiles.Random().GetPositionRT();
    }
}
