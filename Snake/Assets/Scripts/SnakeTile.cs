using UnityEngine;
using System.Collections;


public class SnakeTile : MonoBehaviour
{
    private DestroyEffect destroyEffect;
    private BoardTile boardTile;
    private RectTransform rt;
    private Animator anim;

    void Awake()
    {
        destroyEffect = GetComponent<DestroyEffect>();
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

    public void DisableRigidbody()
    {
        GetComponent<Rigidbody2D>().Sleep();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            collision.GetComponent<DestroyEffect>().Effect();
            GameManager.instance.PlayerAteFood();
        }
        else if (collision.tag == "Player" || collision.tag == "Obstacle")
            GameManager.instance.GameOver();
    }

    public void PlayEating()
    {
        anim.SetTrigger("eat");
    }

    public void PlayDeath()
    {
        anim.SetTrigger("death");
        destroyEffect.Effect();
    }
}
