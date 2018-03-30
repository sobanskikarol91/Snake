using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static Board instance;
    public GameObject tilePrefab;
    public int Columns { get { return columns; } }
    public int Rows { get; private set; }
    public List<BoardTile[]> BoardTiles { get; private set; }
    public List<BoardTile[]> UnusedTiles { get; private set; }

    [SerializeField] private int columns = 21;
    private BoardTileAnimator[] tilesAnimators;
    private Animator anim;
    private GridLayoutGroup gridLayout;

    void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        gridLayout = GetComponent<GridLayoutGroup>();
        SetRowsAndScaleFactorDependsToColumns();

        tilesAnimators = new BoardTileAnimator[Rows * Columns];
        BoardTiles = new List<BoardTile[]>();
    }

    private void Start()
    {
        if (!GameManager.instance.instantStart)
            anim.SetTrigger("showBoard");
    }

    void SetRowsAndScaleFactorDependsToColumns()
    {
        Vector2 spacing = gridLayout.spacing;
        Vector2 sizePrefab = tilePrefab.GetComponent<RectTransform>().rect.size;
        CanvasScaler canvasScaler = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasScaler>();

        float desireWidth = (sizePrefab.x + spacing.x) * Columns;
        canvasScaler.scaleFactor = Screen.width / desireWidth;

        float tileHeight = sizePrefab.y + spacing.y;
        float rowHeight = Screen.height * 1 / canvasScaler.scaleFactor;
        Rows = (int)(rowHeight / tileHeight);
    }

    public IEnumerator CreateBoard()
    {
        CreateTiles();

        if (!GameManager.instance.instantStart)
            while (true)
            {
                if (tilesAnimators.All(t => t.PlayingAnimationIsDone))
                    break;

                yield return null;
            }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
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

        UnusedTiles = BoardTiles;
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        gridLayout.enabled = false;
    }

    public BoardTile GetFirstTilePosition()
    {
        return BoardTiles[0][0];
    }

    public BoardTile GetBoardTile(Vector2Int index)
    {
        return BoardTiles[index.x][index.y];
    }

    public Vector3 GetBoardTilePosition(Vector2Int index)
    {
        return BoardTiles[index.x][index.y].GetPositionRT();
    }

    public Vector3 GetRandomFreeTilePosition()
    {
        List<BoardTile> freeTiles = new List<BoardTile>();

        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Columns; c++)
                if (BoardTiles[r][c].IsFree)
                    freeTiles.Add(BoardTiles[r][c]);

        return freeTiles.Random().GetPositionRT();
    }

    public void ChooseTileToUse(Vector2Int index)
    {
        if (!UnusedTiles[index.x][index.y].IsFree)
            Debug.LogError("Tile is already used: " + index.y + " " + index.x + " " + UnusedTiles[index.y][index.x].IsFree);

        UnusedTiles[index.x][index.y].IsFree = false;
    }

    public void Restart()
    {
        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Columns; c++)
                BoardTiles[r][c].IsFree = true;
    }
}
