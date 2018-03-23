using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startGame = true;
    public bool spawnPlayer = true;
    public Board board;
    public Snake snake;
    public SpawnManager spawnManager;
    public static GameManager instance;
    public SnakeLabel snakeLabel;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (startGame)
            StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return StartCoroutine(board.CreateBoard());
        snakeLabel.CreateLabel();
        if (spawnPlayer)
        {
            snake.CreateSnake();
            spawnManager.SpawnFood();
        }
    }

    //test
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene(0);
    }

    public void GameOver()
    {

    }
}
