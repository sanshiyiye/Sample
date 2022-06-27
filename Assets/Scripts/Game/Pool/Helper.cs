/**
* @classdesc Helper
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using UnityEngine;

namespace Pool
{
    public static class Helper
    {
        public static bool isNullGameObject(this GameObject gameobject)
        {
            return System.Object.ReferenceEquals(gameobject, null);
        }
    }
    
}