using UnityEngine.UI;

public class GameOverMenu : Menu
{
    public Text scoreTxt;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        if(GameManager.instance == null) return;
        scoreTxt.text = "Score: " + GameManager.instance.Score.ToString();
    }
}
