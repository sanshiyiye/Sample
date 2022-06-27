/*
* @classdesc TileInfo
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using UnityEngine;

public class TileInfo : ScriptableObject
{
      [SerializeField]
      private string _name;
      public int gridX;
      public int gridY;
      

     public string Name
     {
         get => _name;
         set => _name = value;
     }
}
