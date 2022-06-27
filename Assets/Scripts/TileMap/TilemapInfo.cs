/*
* @classdesc TilemapInfo
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
public class TilemapInfo : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    
    public List<TileInfo> tilemapList;
    
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public int Width
    {
        get => _width;
        set => _width = value;
    }

    public int Height
    {
        get => _height;
        set => _height = value;
    }

    public void AppendTile(TileInfo tileInfo)
    {
        if (tilemapList == null)
        {
            tilemapList = new List<TileInfo>();
        }
        tilemapList.Add(tileInfo);

       
    }
    
    // #if UNITY_EDITOR
    // private void Awake()
    // {
    //     Init();
    // }
    //
    // private void OnValidate()
    // {
    //     Init();
    // }
    //
    // private void Reset()
    // {
    //     Init();
    // }
    //
    // private void OnDestroy()
    // {
    //     EditorApplication.update -= DelayedInit;
    // }
    //
    // private void Init()
    // {
    //     // If child is already set -> nothing to do
    //     if (tilemapList == null)
    //     {
    //         return;
    //     }
    //
    //     // If this asset already exists initialize immediately
    //     if (AssetDatabase.Contains(this))
    //     {
    //         DelayedInit();
    //     }
    //     // otherwise attach a callback to the editor update to re-check repeatedly until it exists
    //     // this means it is currently being created an the name has not been confirmed yet
    //     else
    //     {
    //         EditorApplication.update -= DelayedInit;
    //         EditorApplication.update += DelayedInit;
    //     }
    // }
    //
    // private void DelayedInit()
    // {
    //     // if this asset dos still not exist do nothing
    //     // this means it is currently being created and the name not confirmed yet
    //     if (!AssetDatabase.Contains(this))
    //     {
    //         return;
    //     }
    //
    //     // as soon as the asset exists remove the callback as we don't need it anymore
    //     EditorApplication.update -= DelayedInit;
    //
    //     // first try to find existing child within all assets contained in this asset
    //     var assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
    //     // you could as well use a loop but this Linq query is a shortcut for finding the first sub asset
    //     // of type "Child" or "null" if there was none
    //     // tilemapList = assets.FirstOrDefault(a => a.GetType() == typeof(Child)) as Child;
    //
    //     // did we find a child ?
    //     if (tilemapList != null)
    //     {
    //         foreach (var tileinfo in tilemapList)
    //         {
    //             var info = CreateInstance<TileInfo>();
    //             info.name = tileinfo.Name;
    //             info.Name = tileinfo.Name;
    //             info.gridX = tileinfo.gridX;
    //             info.gridY = tileinfo.gridY;
    //             AssetDatabase.AddObjectToAsset(info, this);
    //         }
    //         // If not create a new child
    //         
    //         // just for convenience I'd always give assets a meaningful name
    //         // Attach child to the container
    //         
    //     }
    //     
    //     // Mark this asset as dirty so it is correctly saved in case we just changed the "child" field
    //     // without using the "AddObjectToAsset" (which afaik does this automatically)
    //     EditorUtility.SetDirty(this);
    //
    //     // Save all changes
    //     AssetDatabase.SaveAssets();
    // }
    // #endif
    
}