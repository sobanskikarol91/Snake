using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool startGame = true;
    public bool spawnPlayer = true;
    public bool showLabel = true;
    public bool instantStart = true;
    public Board board;
    public Snake snake;
    public SpawnManager spawnManager;
    public static GameManager instance;
    public SnakeLabel snakeLabel;
    public RectTransform spawnHolder;
    public float delayAfterRestart = 1f;
    
    private ColorManager colorManager;
    private MenuManager menu;

    public int Score { get; set; }
    public int scoreFactor = 100;

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

    void AndroidInput()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Vector2 screenTouchPos = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            Vector2 wolrdTouchPos = Camera.main.ScreenToWorldPoint(screenTouchPos);
        }
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
