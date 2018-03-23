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
        boardTile.IsFree = true;
        Destroy(gameObject);
    }

    public void SetPositionToBoard(BoardTile bt)
    {
        boardTile = bt;
        rt.position = bt.GetPositionRT();
        boardTile.IsFree = false;
    }

    public Vector2Int GetIndexInBoard()
    {
        return boardTile.IndexOnBoard;
    }

    public void DisableCollision()
    {
        GetComponent<Rigidbody2D>().Sleep();
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            GetComponentInParent<Snake>().AteFood();
        }
        if (collision.tag == "Player")
            GameManager.instance.GameOver();
    }
}
