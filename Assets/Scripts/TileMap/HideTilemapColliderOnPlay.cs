/*
* @classdesc HideTilemapColliderOnPlay
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTilemapColliderOnPlay : MonoBehaviour
{

    private TilemapRenderer tilemapRenderer;

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapRenderer.enabled = false;
    }
}