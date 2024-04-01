/*
* @classdesc TileMapEditor2
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using FrameWork.Runtime;
using UnityEditor;
using Core.Log;
using Sirenix.Serialization;
using TileMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using Logger = Core.Log.Logger;

public class TileMapEditor : EditorWindow
{

    [MenuItem("Moonjoy/TileMap/ExportGrid")]
    public static void ExportGrid()
    {
        // var currentScene = EditorSceneManager.GetActiveScene().path;
        GameObject gridObj = GameObject.Find("Grid");
        if (gridObj)
        {
            GridInfo gridInfo = CreateGridData(gridObj);
            // return;
            if (gridInfo != null)
            {
                Tilemap[] tilemaps =  gridObj.GetComponentsInChildren<Tilemap>();
                // HashSet<string> tileKeys = new HashSet<string>();
                SortedSetST<string> tileKeys = new SortedSetST<string>();
                
                List<TilemapInfo> tilemapInfos = new List<TilemapInfo>();
                List<Dictionary<int[], string>> tileMapArrayList = new List<Dictionary<int[], string>>();
                for (int i = 0; i < tilemaps.Length; i++)
                {
                    TilemapInfo tilemapInfo= CreateTileMapInfo(tilemaps[i],out Dictionary<int[], string> tilemapDic);
                    tilemapInfos.Add(tilemapInfo);
                    
                    tileMapArrayList.Add(tilemapDic);
                    tileKeys.UnionWith(tilemapDic.Values);
                }
                string path = GameEnv.GameResourceBuildDir+GameEnv.ConfigPath;
                
                for (int i = 0; i < tilemapInfos.Count; i++)
                {
                    TilemapInfo tilemapInfo = tilemapInfos[i];
                    Dictionary<int[], string> tilemapDic = tileMapArrayList[i];
                    
                    foreach (var tile in tilemapDic)
                    {
                        TileInfo tileInfo = ScriptableObject.CreateInstance<TileInfo>();;
                        tileInfo.gridX = tile.Key[0];
                        tileInfo.gridY = tile.Key[1];
                        tileInfo.Name = ""+tileKeys.IndexOf(tile.Value);
                        tileInfo.name = tileInfo.Name;
                        tilemapInfo.AppendTile(tileInfo);
                        // tileInfo.name =
                    }
                    var apath = path + Path.DirectorySeparatorChar+tilemapInfo.Name+".asset";
                    AssetDatabase.CreateAsset(tilemapInfo, apath);
                }
                // AssetDatabase.CreateAsset(tileKeys.ToArray(),path);
                TileKeyMapInfo keyMapInfo = ScriptableObject.CreateInstance<TileKeyMapInfo>();
                keyMapInfo.Keys = tileKeys;
                var tilePath = path + Path.DirectorySeparatorChar + "Tiles.asset";
                AssetDatabase.CreateAsset(keyMapInfo,tilePath);
            }
            
        }

    }

    private static TilemapInfo CreateTileMapInfo(Tilemap tilemap, out Dictionary<int[], string> tilesMap )
    {
        TilemapInfo tilemapInfo = ScriptableObject.CreateInstance<TilemapInfo>();
        tilemapInfo.Name = tilemap.name;
        tilemapInfo.Width = tilemap.size.x;
        tilemapInfo.Height = tilemap.size.y;
        tilesMap = new Dictionary<int[], string>();
        // BoundsInt bounds = tilemap.cellBounds;
        // TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        // Debug.Log("==============" + tilemapInfo.Name+"=============="+bounds.size.x +","+bounds.size.y);
        // for (int x = 0; x < bounds.size.x; x++) {
        //     for (int y = 0; y < bounds.size.y; y++) {
        //         TileBase tile = allTiles[x + y * bounds.size.x];
        //         if (tile != null) {
        //             Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
        //         } else {
        //             Debug.Log("x:" + x + " y:" + y + " tile: (null)");
        //         }
        //     }
        // } 
        // Debug.Log("==============" + tilemap.size.x + "," + tilemap.size.y);
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int gridPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(gridPlace))
            {
                TileBase tile = tilemap.GetTile(gridPlace);
                tilesMap.Add(new int[]{pos.x,pos.y},tile.name);
            }
        }

        return tilemapInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gridm"></param>
    private static GridInfo CreateGridData(GameObject gridObj)
    {
        Grid mGrid = gridObj.GetComponent<Grid>();
        if (mGrid == null)
        {
            Logger.Error(LogLevel.EDITOR,"TileMapEditor:CreateGridData {0} is not a grid component!",gridObj.name);
            return null;
        }
        GridInfo gridInfo = ScriptableObject.CreateInstance<GridInfo>();
        gridInfo.CellLayOut = mGrid.cellLayout;
        gridInfo.CellSize = mGrid.cellSize;
        // *注意 为了模块划分牺牲一次遍历，方便阅读不进行构造器设计 
        Tilemap[] tilemaps =  gridObj.GetComponentsInChildren<Tilemap>();
        for (int i = 0; i < tilemaps.Length; i++)
        {
            gridInfo.AddTileAssetName(tilemaps[i].transform.name);
        }
        
        string path = GameEnv.GameResourceBuildDir+GameEnv.ConfigPath;
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
 
        // *注意 AssetDatabase.CreateAsset 方法使用的是相对路径
        path += $"/Grid.asset";
        AssetDatabase.CreateAsset(gridInfo, path);
        
        // byte[] bytes = SerializationUtility.SerializeValue(gridInfo, DataFormat.Binary);
        // File.WriteAllBytes(path,bytes);
        
        return gridInfo;
    }
    
    
    private static void Deserialize () {
        string path = GameEnv.GameResourceBuildDir+GameEnv.ConfigPath;
        path += $"/Grid.asset";
        GridInfo gridInfo = AssetDatabase.LoadAssetAtPath<GridInfo>(path);
     
    }
    
    [MenuItem("Moonjoy/TileMap/ExportGridBinary")]
    private static void BinarySerialize () {
        Rect rect = new Rect();
        rect.width = 10;
        rect.height = 20;
 
        string path = Application.dataPath + "/Data/Rect.bytes";
        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream,rect);
        fileStream.Close();
 
        AssetDatabase.Refresh(); // 保存到 Assets 文件夹下需要刷新才能看到
    }
 
    private static void BinaryDeserialize () {
        // 非 Assets 文件夹下时，使用 byte[] bytes = File.ReadAllBytes(path);
        TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Data/Rect.bytes");
		
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
		
        Rect rect = (Rect)binaryFormatter.Deserialize(memoryStream);
        memoryStream.Close();
		
        Debug.Log(rect.width);  // 10
        Debug.Log(rect.height); // 20
    }
}
#endif