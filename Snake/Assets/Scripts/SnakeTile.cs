using UnityEngine;
using System.Collections;
public class SnakeTile : MonoBehaviour
{
    private BoardTile boardTile;
    private RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    public void RemoveTail()
    {
        Destroy(gameObject);
    }

    public void SetPositionToBoard(BoardTile bt)
    {
        boardTile = bt;
        rt.position = bt.GetPositionRT();
    }

    public Vector2Int GetIndexInBoard()
    {
        return boardTile.IndexOnBoard;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            GetComponentInParent<Snake>().AteFood();
        }
        if (collision.tag == "Player")
        {
            Debug.LogError("GameOver");
        }
    }
}
