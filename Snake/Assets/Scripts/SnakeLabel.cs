using UnityEngine;
using System.Collections.Generic;

// Logo Creator
public class SnakeLabel : MonoBehaviour
{
    public Transform spawnHolder;
    public GameObject labelPrefab;
    public float timeToDestroy = 3.1f;
    private List<Vector2Int> tilesIndexes = new List<Vector2Int>();
    
    readonly Vector2Int size = new Vector2Int(19,5);
    private List<DestroyEffect> labelDestroyEffects = new List<DestroyEffect>(); 

    public void CreateLabel()
    {
        GetIndexes();
        CreateLetters();
        Invoke("DestroyIt", timeToDestroy);
    }

    Vector2Int CenterLabel()
    {
        return new Vector2Int((Board.instance.Rows - size.y) / 2, (Board.instance.Columns - size.x) / 2);
    }

    void CreateLetters()
    {
        foreach (Vector2Int vector in tilesIndexes)
        {
            GameObject go = Board.instance.GetBoardTile(vector + CenterLabel()).gameObject;
            GameObject newGo = Instantiate(labelPrefab);
            labelDestroyEffects.Add(newGo.GetComponent<DestroyEffect>());
            newGo.transform.SetParent(spawnHolder);
            RectTransform newGoRT = newGo.GetComponent<RectTransform>();
            newGoRT.localPosition = go.GetComponent<RectTransform>().localPosition;
            newGoRT.localScale = Vector3.one;

            newGo.GetComponent<Animator>().enabled = false;
        }
    }

    void DestroyIt()
    {
        labelDestroyEffects.ForEach(t => t.Effect());
    }

    void GetIndexes()
    {
        Row0();
        Row1();
        Row2();
        Row3();
        Row4();
    }

    void Row0()
    {
        tilesIndexes.Add(new Vector2Int(0, 0));
        tilesIndexes.Add(new Vector2Int(0, 1));
        tilesIndexes.Add(new Vector2Int(0, 2));
        tilesIndexes.Add(new Vector2Int(0, 4));
        tilesIndexes.Add(new Vector2Int(0, 5));
        tilesIndexes.Add(new Vector2Int(0, 6));
        tilesIndexes.Add(new Vector2Int(0, 8));
        tilesIndexes.Add(new Vector2Int(0, 9));
        tilesIndexes.Add(new Vector2Int(0, 10));
        tilesIndexes.Add(new Vector2Int(0, 12));
        tilesIndexes.Add(new Vector2Int(0, 14));
        tilesIndexes.Add(new Vector2Int(0, 16));
        tilesIndexes.Add(new Vector2Int(0, 17));
        tilesIndexes.Add(new Vector2Int(0, 18));
    }

    void Row1()
    {
        tilesIndexes.Add(new Vector2Int(1, 0));
        tilesIndexes.Add(new Vector2Int(1, 4));
        tilesIndexes.Add(new Vector2Int(1, 6));
        tilesIndexes.Add(new Vector2Int(1, 8));
        tilesIndexes.Add(new Vector2Int(1, 10));
        tilesIndexes.Add(new Vector2Int(1, 12));
        tilesIndexes.Add(new Vector2Int(1, 14));
        tilesIndexes.Add(new Vector2Int(1, 16));
    }

    void Row2()
    {
        tilesIndexes.Add(new Vector2Int(2, 0));
        tilesIndexes.Add(new Vector2Int(2, 1));
        tilesIndexes.Add(new Vector2Int(2, 2));
        tilesIndexes.Add(new Vector2Int(2, 4));
        tilesIndexes.Add(new Vector2Int(2, 6));
        tilesIndexes.Add(new Vector2Int(2, 8));
        tilesIndexes.Add(new Vector2Int(2, 9));
        tilesIndexes.Add(new Vector2Int(2, 10));
        tilesIndexes.Add(new Vector2Int(2, 12));
        tilesIndexes.Add(new Vector2Int(2, 13));
        tilesIndexes.Add(new Vector2Int(2, 16));
        tilesIndexes.Add(new Vector2Int(2, 17));
        tilesIndexes.Add(new Vector2Int(2, 18));
    }

    void Row3()
    {
        tilesIndexes.Add(new Vector2Int(3, 2));
        tilesIndexes.Add(new Vector2Int(3, 4));
        tilesIndexes.Add(new Vector2Int(3, 6));
        tilesIndexes.Add(new Vector2Int(3, 8));
        tilesIndexes.Add(new Vector2Int(3, 10));
        tilesIndexes.Add(new Vector2Int(3, 12));
        tilesIndexes.Add(new Vector2Int(3, 14));
        tilesIndexes.Add(new Vector2Int(3, 16));
    }

    void Row4()
    {
        tilesIndexes.Add(new Vector2Int(4, 0));
        tilesIndexes.Add(new Vector2Int(4, 1));
        tilesIndexes.Add(new Vector2Int(4, 2));
        tilesIndexes.Add(new Vector2Int(4, 4));
        tilesIndexes.Add(new Vector2Int(4, 6));
        tilesIndexes.Add(new Vector2Int(4, 8));
        tilesIndexes.Add(new Vector2Int(4, 10));
        tilesIndexes.Add(new Vector2Int(4, 12));
        tilesIndexes.Add(new Vector2Int(4, 14));
        tilesIndexes.Add(new Vector2Int(4, 16));
        tilesIndexes.Add(new Vector2Int(4, 17));
        tilesIndexes.Add(new Vector2Int(4, 18));
    }
}
