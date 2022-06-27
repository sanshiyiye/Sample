/*
* @classdesc TileKeyMapInfo
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.Collections.Generic;
using FrameWork.Utils;
using UnityEditor;
using UnityEngine;

namespace TileMap
{
    public class TileKeyMapInfo : ScriptableObject
    {
        [SerializeField]
        private SortedSetST<string> keys;
        

        public SortedSetST<string> Keys
        {
            get => keys;
            set => keys = value;
        }
    }

    public class TileKeyInfo
    {
        public string Name;
        public int index;
        
    }
}