/*
* @classdesc GridInfo
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridInfo : ScriptableObject
{

    [SerializeField] public GridLayout.CellLayout CellLayOut;

    [SerializeField] public Vector3 CellSize;
    // [SerializeField]
    // public List<string> TilemapNames = new List<string>();
    public List<string> tilemapList = new  List<string>(6);

    public void AddTileAssetName(string tileName)
    {
        tilemapList.Add(tileName);
    }
    
    // public GridLayout.CellLayout CellLayoutType()
    // {
    //     switch (CellLayOut)
    //     {
    //         case 0:
    //             return GridLayout.CellLayout.Rectangle;
    //         case 1:
    //             return GridLayout.CellLayout.Hexagon;
    //         case 2:
    //             return GridLayout.CellLayout.Isometric;
    //         case 3:
    //             return GridLayout.CellLayout.IsometricZAsY;
    //         default:
    //             return GridLayout.CellLayout.Isometric;
    //     }
    //     
    // }
}