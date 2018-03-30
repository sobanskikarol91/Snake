using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour
{
    [Range(5,20)]
    public float timeToChangeColor = 9f;
    private Image[,] boardImages;
    private List<Color> colors = new List<Color>();
    private int rows;
    private int columns;

    public void StartChangingColor()
    {
        AddColorsToList();
        GetBoardSize();
        GetBoardImages();
        StartCoroutine(ColorSystem());
    }

    public void GetBoardSize()
    {
        rows = Board.instance.Rows;
        columns = Board.instance.Columns;
    }

    void AddColorsToList()
    {
        colors.Add(new Color32(86, 00, 0, 255));
        colors.Add(new Color32(0, 100, 30, 255));
        colors.Add(new Color32(80, 84, 0, 255));
        colors.Add(new Color32(98, 0, 95, 255));
        colors.Add(new Color32(0, 21, 98, 255));
        colors.Add(Color.black);
    }

    void GetBoardImages()
    {
        boardImages = new Image[rows, columns];

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < columns; c++)
                boardImages[r, c] = Board.instance.BoardTiles[r][c].GetComponent<Image>();
    }

    IEnumerator ColorSystem()
    {
        Coroutine coroutine;

        while (true)
        {
            coroutine = StartCoroutine(ChangeColor());
            yield return new WaitForSeconds(timeToChangeColor);
            StopCoroutine(coroutine);
        }
    }

    IEnumerator ChangeColor()
    {
        Color32 color = boardImages[0, 0].color;
        Color32 destination = colors.Random();

        while (true)
        {
            color = Color32.Lerp(color, destination, Time.deltaTime);

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                    boardImages[r, c].color = color;

            yield return null;
        }
    }
}
