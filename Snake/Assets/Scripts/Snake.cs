using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// PlayerController
public class Snake : MonoBehaviour
{
    public GameObject snakePrefab;
    public SnakeTile head;
    public AudioSource moveAS;
    public AudioSource foodAS;
    [Range(0, 2)] public float startMoveTime = 0.2f;
    public float currentMoveTime;
    public float increaseSpeedFactor = 0.98f;
    public int startSnakeLength = 7;

    private int currentSnakeLength;
    private int maxSnakeLength;
    private DIRECTION direction;
    private List<SnakeTile> tilesList = new List<SnakeTile>();
    private bool isPlayerchoseDirection;
    private bool isAlive = true;

    [SerializeField] private SnakeAnimation anim;


    public void CreateSnake()
    {
        if (!isAlive) Restart();
        direction = DIRECTION.East;
        currentSnakeLength = 0;
        maxSnakeLength = startSnakeLength;
        currentMoveTime = startMoveTime;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (isAlive)
        {
            moveAS.Play();

            CreateNextTile();

            if (currentSnakeLength >= maxSnakeLength)
                RemoveTail();

            isPlayerchoseDirection = false;
            yield return new WaitForSeconds(currentMoveTime);
        }
    }

    public void CreateNextTile()
    {
        // Create new snake Tile
        SnakeTile newSnakeTile = Instantiate(snakePrefab).GetComponent<SnakeTile>();
        // Set parent to Snake holder
        newSnakeTile.transform.SetParent(transform);
        // Change scale to 1
        newSnakeTile.transform.localScale = new Vector3(1, 1, 1);
        // Set RT world position relative to last snake tile - head
        if (head != null)
        {
            Vector2Int index = GetPositionToCreateTile();
            BoardTile bt = Board.instance.GetBoardTile(index);
            newSnakeTile.SetPositionToBoard(bt);
            head.DisableRigidbody();
        }
        else
            newSnakeTile.SetPositionToBoard(Board.instance.GetFirstTilePosition());

        SnakeTile snakeTile = newSnakeTile.GetComponent<SnakeTile>();
        head = snakeTile;
        tilesList.Add(snakeTile);
        currentSnakeLength++;
    }

    void IncreaseSpeed()
    {
        currentMoveTime *= increaseSpeedFactor;
    }

    void Update()
    {
        if (isPlayerchoseDirection) return;

        if (Application.platform == RuntimePlatform.Android)
            AndroidMove();
        else
            PCMove();
    }

    void PCMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0)
        {
            if (IsMovingHorizontal()) return;
            direction = horizontal > 0 ? DIRECTION.East : DIRECTION.West;
            isPlayerchoseDirection = true;
            return;
        }

        if (vertical != 0)
        {
            if (IsMovingVertical()) return;
            direction = vertical > 0 ? DIRECTION.North : DIRECTION.South;
            isPlayerchoseDirection = true;
        }
    }

    void AndroidMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPlayerchoseDirection = true;
            Vector2 headPos = head.GetPositionOnScreen();

            if (IsMovingHorizontal())
                direction = Input.mousePosition.y > headPos.y ? DIRECTION.North : DIRECTION.South;
            else
                direction = Input.mousePosition.x > headPos.x ? DIRECTION.East : DIRECTION.West;
        }
    }

    bool IsMovingHorizontal()
    {
        return direction == DIRECTION.East || direction == DIRECTION.West;
    }

    bool IsMovingVertical()
    {
        return direction == DIRECTION.North || direction == DIRECTION.South;
    }

    Vector2Int GetDirectionToCreateTile()
    {
        switch (direction)
        {
            case DIRECTION.North:
                return Vector2Int.left;
            case DIRECTION.South:
                return Vector2Int.right;
            case DIRECTION.West:
                return Vector2Int.down;
            default:
                return Vector2Int.up;
        }
    }

    Vector2Int GetPositionToCreateTile()
    {
        Vector2Int direction = head.GetIndexInBoard() + GetDirectionToCreateTile();

        if (direction.x >= Board.instance.Rows) direction.x = 0;
        else if (direction.x < 0) direction.x = Board.instance.Rows - 1;
        else if (direction.y >= Board.instance.Columns) direction.y = 0;
        else if (direction.y < 0) direction.y = Board.instance.Columns - 1;

        return direction;
    }

    void RemoveTail()
    {
        SnakeTile st = tilesList.First();
        tilesList.Remove(st);
        st.RemoveTail();
        currentSnakeLength--;
    }

    public void AteFood()
    {
        anim.PlayEating(tilesList);
        maxSnakeLength++;
        foodAS.Play();
        IncreaseSpeed();
    }

    public void Death()
    {
        anim.PlayDeath(tilesList, currentSnakeLength);
        StopAllCoroutines();
        isAlive = false;
    }

    public void Restart()
    {
        isAlive = true;
        head = null;
        tilesList.Clear();
    }
}
