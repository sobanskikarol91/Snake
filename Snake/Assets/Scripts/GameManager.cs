using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool startGame = true;
    public bool spawnPlayer = true;
    public bool showLabel = true;
    public Board board;
    public Snake snake;
    public SpawnManager spawnManager;
    public static GameManager instance;
    public SnakeLabel snakeLabel;
    public RectTransform spawnHolder;

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
        if (showLabel)
        {
            snakeLabel.CreateLabel();
            yield return new WaitForSeconds(3.5f);
        }
 

        if (spawnPlayer)
        {
            snake.CreateSnake();
            spawnManager.Spawn();
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
        snake.Death();
    }
}
