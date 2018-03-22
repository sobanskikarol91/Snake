using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour
{
    private Board board;
    /*
    IEnumerator ChangeColor()
    {
        Color32 color = tilePrefab.GetComponent<Image>().color;

        while (true)
        {
            color = Color32.Lerp(color, new Color32(1, 23, 3, 255), Time.deltaTime);

            foreach (var t in Tiles)
            {
                for (int i = 0; i < Columns; i++)
                {
                    t[i].GetComponent<Image>().color = color;
                }
            }
            yield return null;
        }
    }
    */
}
