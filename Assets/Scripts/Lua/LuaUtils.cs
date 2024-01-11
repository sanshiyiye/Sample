/*
* @classdesc LuaUtils
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using UnityEngine;

public class LuaUtils
{

    public static void SetPos(GameObject obj, float x, float y, float z)
    {
        obj.transform.position = new Vector3(x, y, z); 
    }
}
