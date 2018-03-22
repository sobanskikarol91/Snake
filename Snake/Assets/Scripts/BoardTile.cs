﻿using System;
using UnityEngine;
using System.Collections;

public class BoardTile : MonoBehaviour
{
    public bool IsFree { get; set; }
    public Vector2Int IndexOnBoard { get; set; }
    private RectTransform rt;

    public int row;
    public int col;

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
