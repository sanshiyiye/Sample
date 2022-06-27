using UnityEngine;

/**
* @classdesc CacheDataInfo
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public class CacheDataInfo
{

    public CacheDataInfo(string name, Object obj)
    {
        CacheName = name;
        CacheObject = obj;
        StartTick = Time.realtimeSinceStartup;

    }
    
    public void Retain()
    {
        Ref = Ref + 1;
    }

    public void Release()
    {
        Ref = Ref - 1;
        Ref = Ref <= 0 ? 0 : Ref;
    }
    
    
    public Object CacheObject { get; set; }
    public double StartTick { get; set; }
    public object CacheName { get; set; }
    
    public int Ref { get; set; }
}
