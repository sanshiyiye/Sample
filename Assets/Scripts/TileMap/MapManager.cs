/*
* @classdesc MapManager
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.Collections.Generic;
using System.IO;
using Core;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{

        /// <summary>
        /// 无效Vector点
        /// </summary>
        public static Vector3 NagtiveVector = new Vector3(-1000000, -1000000, -1000000);

        private Grid mGrid;

        public List<Tilemap> mTilemaps;

        public List<GameObject> mapObjects;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Vector3 GetCoord(int x, int y , int z)
        {
            if (mGrid == null)
            {
                
                return NagtiveVector ;
            }

            var point = new Vector3(x, y, 0);
            return mGrid.WorldToCell(point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inX"></param>
        /// <param name="inY"></param>
        /// <param name="inZ"></param>
        /// <returns></returns>
        public Vector3 GetLocalPos(int inX, int inY, int inZ)
        {
            if (mGrid == null)
            {
                
                return NagtiveVector;
            }

            var gridIndex = new Vector3Int(inX, inY, inZ);
            return mGrid.CellToLocal(gridIndex);
        }

        /* 使用的类 */
        // public RuleTile tile_1; // ruleTile 对应的类，该类继承自 TileBase，意味着它也可以使用父类的一些基本方法
        public Tilemap tilemap_1;   // tilemap 对应的类，该类继承自 GridLayout，可以使用父类的一些基本方法
        public void RoleTile(Vector3Int pos ,Vector3 worldPos)
        {
       
        /* 放置与删除指定位置瓦块 */
        // tilemap_1.SetTile(pos, tile_1); // 将'tile_1'瓦块放置到'tilemap_1'瓦块地图的'pos'位置上
        tilemap_1.SetTile(pos, null);   // 将 tilemap_1'瓦块地图的'pos'位置上的瓦块清空

/* 位置转换 */ 
        Vector3Int currentPos = tilemap_1.WorldToCell(worldPos); // 将对应的'worldPos'世界坐标转换为对应的瓦块地图的坐标，为 Vector3Int 类型

/* 获取位置上的瓦块 */
        TileBase tile_2 = tilemap_1.GetTile(currentPos);
        // RuleTile tile_3 = tilemap_1.GetTile<RuleTile>(currentPos);  // 获取'tilemap_1'中某个单元格的给定'currentPos'位置处的'tile of type RuleTile'
        }
        
        /// <summary>
        /// 旋转
        /// </summary>
        public void TransLate()
        {
            
        }
        /// <summary>
        /// 初始化地图数据
        /// </summary>
        public void init()
        {
            //TODO init Grid
            //TODO load Map
            
        }

        public void CreateGrid(GameObject obj)
        {
            GameObject gridGameObject = new GameObject("Grid");
            mGrid = gridGameObject.AddComponent<Grid>();
            mGrid.cellLayout = GridLayout.CellLayout.IsometricZAsY;
            mGrid.cellSize = new Vector3(1, 0.5f, 1);
            gridGameObject.transform.parent = obj.transform;
        }

        public void CreateTileMaps()
        {
            
        }   
        
        
        //TODO 优化数据结构
        private Dictionary<string, List<Dictionary<string, string>>> AllCfgs = new Dictionary<string, List<Dictionary<string, string>>>();

        /// <summary>
        /// 
        /// </summary>
        public void LoadMapData()
        {
            //TODO load配置资源
            AddTilesTable("BundleResources/Config/Tilemap/Tiles.txt");
            AddPrefabTable("BundleResources/Config/Tilemap/Prefabs.txt");
            AddMapTable("BundleResources/Config/Tilemap/Maps.txt");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfgPath"></param>
        public void AddTilesTable(string cfgPath)
        {
            string name = "TilesTable";
            this.AddTable(name, cfgPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfgPath"></param>
        public void AddPrefabTable(string cfgPath)
        {
            string name = "PrefabTable";
            this.AddTable(name, cfgPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfgPath"></param>
        public void AddMapTable(string cfgPath)
        {
            string name = "MapTable";
            this.AddTable(name, cfgPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cfgPath"></param>
        public void AddTable(string name, string cfgPath)
        {
            string context = LoadCfgFile(cfgPath);
            List<Dictionary<string, string>> list = AnalyzeCifFile(context);
            this.AllCfgs.Add(name, list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string LoadCfgFile(string path)
        {
            string fileContext = File.ReadAllText(Application.dataPath + "/" + path);
            return fileContext;
        }
        
        /// <summary>
        /// 解析配置文件
        /// </summary>
        /// <param name="cfgFileContext"></param>
        private List<Dictionary<string, string>> AnalyzeCifFile(string cfgFileContext)
        {
            string[] lines = cfgFileContext.Split('\n');
            string[] keys;
            List<Dictionary<string, string>> items = new List<Dictionary<string, string>>();
            bool newItem = false;
            Dictionary<string, string> item = null;
            int keyConst = Int32.Parse(lines[0].Split(' ')[1]);
            keys = new string[keyConst];
            for (int keyIndex = 0; keyIndex < keyConst; keyIndex++)
            {
                keys[keyIndex] = lines[keyIndex + 1];
            }

            int keyI = 0;
            item = new Dictionary<string, string>();
            for (int lineIndex = keyConst + 1; lineIndex < lines.Length; lineIndex++)
            {
                if (lines[lineIndex].StartsWith("#"))
                {
                    // 新的 item
                    if (newItem)
                    {
                        items.Add(item);
                    }
                    item = new Dictionary<string, string>();
                    keyI = 0;
                    newItem = true;
                    continue;
                }
                
                item.Add(keys[keyI], lines[lineIndex]);
                keyI++;
              
            }
            items.Add(item);

            return items;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Dictionary<string, string> FindItem(string tableName, int ID)
        {
            var table = this.AllCfgs[tableName];
            if (table == null)
            {
                return null;
            }

            if(ID >= table.Count)
                return null;
            var item = table[ID];
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, string> FindMapItem(int id)
        {
            return FindItem("MapTable", id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, string> FindTileItem(int id)
        {
            return FindItem("TilesTable", id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, string> FindPrefabItem(int id)
        {
            return FindItem("PrefabTable", id);
        }
#region 流序列化
        /**
        public static string savePath = "mapData/";
        /// <summary>
        /// 简单使用二进制序列化数据
        /// </summary>
        /// <param name="path"></param>
        private void BinarySerilizeMap(string path) 
        {
            
            var data = new TilemapInfo
            {
            
                Width = 1280,
                Height = 720,
                Name = "",
            };

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);

            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs,data);

            fs.Close();

            Debug.Log("存储成功");
        }


        /// <summary>
        /// 反序列化数据
        /// </summary>
        /// <returns></returns>
        private TilemapInfo BinaryDeSerilizeMap() 
        {
            
            if (!File.Exists(savePath))
            {
            
                //文件不存在
                Debug.Log("先存张地图再加载：按下空格键就可以存储一张默认的地图");
                return null;
            }

            FileStream fs = new FileStream(savePath, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            var data = (TilemapInfo)bf.Deserialize(fs);
            fs.Close();
            return data;
        }
        
        **/
        ///
        /// ClearAllTiles() 清除所有单元格数据
        ///GetCellCenterLocal() 获取每个单元格的本地坐标
        ///    GetCellCenterWorld() 获取每个单元格的世界坐标
        ///    GetSprite(Vector3Int position)  获取指定位置的图片信息  
         ///   GetTile() 获取指定位置的单元格的信息
         ///   RefreshAllTiles() 刷新整张地图
         ///   RefreshTile(Vector3Int position) 刷新指定位置的单元格
         ///   SetTile()  设定指定位置单元格数据
         ///   SetTiles(Vector3Int[] positionArray, TileBase[] tileArray) 直接使用数组设置多个位置的多张图片
        /// 
#endregion

        
    }