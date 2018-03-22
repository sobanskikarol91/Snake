using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Board board;
    public Snake snake;
    public SpawnManager spawnManager;

    void Start()
    {
        board.CreateBoard();
        snake.CreateSnake();
        spawnManager.StartSpawn();
    }
}
