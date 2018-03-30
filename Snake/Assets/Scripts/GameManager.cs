using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Test Settings")]
    public bool startGame = true;
    public bool spawnPlayer = true;
    public bool showLabel = true;
    public bool instantStart = true;

    [Header("Game Settings")]
    public Board board;
    public Snake snake;
    public SpawnManager spawnManager;
    public static GameManager instance;
    public SnakeLabel snakeLabel;
    public RectTransform spawnHolder;
    public float delayAfterRestart = 1f;
    public int Score { get; set; }
    public int scoreFactor = 100;

    private ColorManager colorManager;
    private MenuManager menu;

    void Awake()
    {
        instance = this;
        colorManager = GetComponent<ColorManager>();
        menu = GetComponent<MenuManager>();
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
            yield return new WaitForSeconds(3.0f);
        }

        if (spawnPlayer)
        {
            snake.CreateSnake();
            spawnManager.Spawn();
            colorManager.StartChangingColor();
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
        menu.ShowGameOver();
    }

    public void RestartGame()
    {
        StartCoroutine(IERestart());    
    }


    IEnumerator IERestart()
    {
        spawnHolder.GetComponent<SpawnHolder>().RemoveSpawnedObjects();
        yield return new WaitForSeconds(delayAfterRestart);
        Score = 0;
        board.Restart();
        snake.CreateSnake();
        spawnManager.Restart();
    }

    public void PlayerAteFood()
    {
        snake.AteFood();
        spawnManager.SpawnFood();
        Score += scoreFactor;
    }
}