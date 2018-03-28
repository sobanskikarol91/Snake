using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Menu gameOver;
    public float delayGameOver = 0.5f;

    void Start()
    {
        gameOver.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        Invoke("DelayedShow", delayGameOver);
    }

    void DelayedShow()
    {
        gameOver.gameObject.SetActive(true);
    }

    void HideGameOver()
    {
        gameOver.gameObject.SetActive(false);
        GameManager.instance.RestartGame();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        Invoke("HideGameOver", 0.4f);
    }
}
