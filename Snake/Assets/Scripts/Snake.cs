﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour
{
    public GameObject snakePrefab;
    public SnakeTile head;


    public AudioSource moveAS;
    public AudioSource foodAS;
    [Range(0, 2)]
    public float moveTime = 0.5f;
    public float maxSnakeLength = 7;

    private int currentSnakeLength;
    private DIRECTION direction = DIRECTION.East;
    private List<SnakeTile> tilesList = new List<SnakeTile>();
    private bool isPlayerchoseDirection;
    private bool isAlive = true;
    [SerializeField] private SnakeAnimation animation;

    public void CreateSnake()
    {
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
            yield return new WaitForSeconds(moveTime);
        }
    }

    private int nr = 0;
    public void CreateNextTile()
    {
        nr++;
        // Create new snake Tile
        SnakeTile newSnakeTile = Instantiate(snakePrefab).GetComponent<SnakeTile>();
        newSnakeTile.name = nr.ToString();
        // Set parent to Snake holder
        newSnakeTile.transform.SetParent(transform);
        // Change scale to 1
        newSnakeTile.transform.localScale = new Vector3(1, 1, 1);

        // Set RT world position relative to last snake tile - head
        if (head != null)
        {
            Vector2Int index = GetPositionToCreateTile();
            BoardTile bt = Board.GetBoardTile(index);
            newSnakeTile.SetPositionToBoard(bt);
            head.DisableCollision();
        }
        else
            newSnakeTile.SetPositionToBoard(Board.GetFirstTilePosition());

        SnakeTile snakeTile = newSnakeTile.GetComponent<SnakeTile>();
        head = snakeTile;
        tilesList.Add(snakeTile);
        currentSnakeLength++;
    }

    void IncreaseSpeed()
    {
        moveTime *= 0.98f;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (isPlayerchoseDirection) return;

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
            case DIRECTION.East:
                return Vector2Int.up;
            default:
                return Vector2Int.down;
        }
    }

    Vector2Int GetPositionToCreateTile()
    {
        Vector2Int direction = head.GetIndexInBoard() + GetDirectionToCreateTile();
        if (direction.x >= Board.Rows) direction.x = 0;
        else if (direction.x < 0) direction.x = Board.Rows - 1;
        else if (direction.y >= Board.Columns) direction.y = 0;
        else if (direction.y < 0) direction.y = Board.Columns - 1;

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
        GameManager.instance.spawnManager.SpawnFood();
        animation.PlayEating(tilesList);
        maxSnakeLength++;
        foodAS.Play();
        IncreaseSpeed();
    }

    public void Death()
    {
        animation.PlayDeath(tilesList);
        isAlive = false;
    }
}
