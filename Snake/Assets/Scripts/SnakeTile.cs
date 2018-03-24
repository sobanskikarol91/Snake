using UnityEngine;
using System.Collections;
public class SnakeTile : MonoBehaviour
{
    private BoardTile boardTile;
    private RectTransform rt;
    private Animator anim;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
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
            collision.GetComponent<DestroyEffect>().Effect();
            GetComponentInParent<Snake>().AteFood();
        }
        if (collision.tag == "Player")
            GameManager.instance.GameOver();
    }

    public void PlayAnimation()
    {
        anim.SetTrigger("eat");
    }

    public void DestroyTile()
    {

    }
}
