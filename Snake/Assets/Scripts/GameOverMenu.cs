using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    public Text scoreTxt;
    protected override void OnEnable()
    {
        base.OnEnable();
        scoreTxt.text = "Score: " + GameManager.instance.Score.ToString();
    }
}
