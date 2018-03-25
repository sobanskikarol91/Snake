using UnityEngine;
using System.Collections;

public class BoardTile : Tile
{
    public bool IsFree { get; set; }

    private RectTransform rt;
    
    void Start()
    {
        rt = GetComponent<RectTransform>();
        IsFree = true;
    }

    public Vector3 GetPositionRT()
    {
        return rt.position;
    }
}
