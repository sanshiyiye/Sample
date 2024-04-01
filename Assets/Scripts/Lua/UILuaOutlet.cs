/*
* @classdesc UILuaOutlet
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[RequireComponent(typeof(FrameWork.Runtime.UIWindowAsset))]
[DisallowMultipleComponent]
#endif
public class UILuaOutlet : MonoBehaviour
{
        public bool FillByObjectName = false;
        /// Outlet info, serialize
        /// </summary>
        [System.Serializable]
        public class OutletInfo
        {
            /// <summary>
            /// Lua Property Name
            /// </summary>
            public string Name;

            /// <summary>
            /// Component type 's full name (with namespace)
            /// </summary>
            public string ComponentType;

            /// <summary>
            /// UI Control Object
            /// </summary>
            public UnityEngine.Object Object;
        }
        /// <summary>
        /// Serialized outlet infos
        /// 可以减少lua端的find操作
        /// </summary>
        public List<OutletInfo> OutletInfos = new List<OutletInfo>();
}
