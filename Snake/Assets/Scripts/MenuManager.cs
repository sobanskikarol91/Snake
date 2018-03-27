using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Menu gameOver;

    void Start()
    {
        gameOver.gameObject.SetActive(false);
    }
    public void ShowGameOver()
    {
        gameOver.gameObject.SetActive(true);
    }

    void HideGameOver()
    {
        gameOver.gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        HideGameOver();
        GameManager.instance.RestartGame();
    }
}
