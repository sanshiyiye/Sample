
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

/**
* @classdesc AssetManager
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
public class AssetManager : MonoSingleton<AssetManager>
{

    public AssetLoadMode loadMode = AssetLoadMode.Editor;
    
    public float ClearCacheDuration;

    public float cacheDataStayTime;
    
    private float cacheTimeTemp;
    
    public IAssetLoader assetLoader;

    private Dictionary<string, CacheDataInfo> cacheDataDic = new Dictionary<string, CacheDataInfo>();
    
    public void init(AssetLoadMode mode, float interval = 10f, float stayTIme = 9f)
    {
#if UNITY_EDITOR
        loadMode = AssetLoadMode.Editor;
        assetLoader = new EditorAssetLoader();
#else
        loadMode = AssetLoadMode.AssetBundler;
        assetLoader = new AssetBundleLoader();
#endif
        // loadMode = mode;
        ClearCacheDuration = interval;
        cacheDataStayTime = stayTIme;
        
    }

    public T LoadAsset<T>(string path) where T : Object
    {
        CacheDataInfo cacheInfo = GetCacheData(path);
        if (cacheInfo != null)
        {
            cacheInfo.Retain();
            return cacheInfo.CacheObject as T;
        }
        else
        {
            return assetLoader.loadAsset<T>(path);
        }
    }

    public void LoadAssetAsync<T>(string path,UnityAction<T> onLoadComplete) where T : class
    {
        CacheDataInfo cacheDataInfo = GetCacheData(path);
        if (cacheDataInfo != null)
        {
            cacheDataInfo.Retain();
            if (onLoadComplete != null)
            {
                onLoadComplete(cacheDataInfo.CacheObject as T);
            }
        }
        else
        {
            assetLoader.LoadAssetAsync<T>(path,onLoadComplete);
        }
        
    }
    
    private CacheDataInfo GetCacheData(string path)
    {
        if (cacheDataDic.ContainsKey(path))
        {
            return cacheDataDic[path];
        }
        return null;
    }

    public void PushCache(string path, Object obj)
    {
        lock (cacheDataDic)
        {
            CacheDataInfo info; 
            if (cacheDataDic.ContainsKey(path))
            {
                info = cacheDataDic[path];
            }
            else
            {
                info = new CacheDataInfo(path, obj);
                cacheDataDic.Add(path,info);
                
            }

            info.Retain();
        }
    }

    public void ClearCache()
    {
        cacheDataDic.Clear();
    }

    public void RemoveCache(string path)
    {
        if(path != null && !path.Trim().Equals(""))
        cacheDataDic.Remove(path);
    }
    //TODO 增加对象计数
    private void Update()
    {
        if (ClearCacheDuration < 0) return;
        
        cacheTimeTemp += Time.deltaTime;

        if (cacheTimeTemp >= ClearCacheDuration)
        {
            foreach (var iter in cacheDataDic.ToList()) {
                if (iter.Value.StartTick + cacheDataStayTime >= Time.realtimeSinceStartup && iter.Value.Ref <=0) {
                    cacheDataDic.Remove(iter.Key);
                }
            }

            cacheTimeTemp -= ClearCacheDuration;
        }
    }
}

public enum AssetLoadMode
{
    Editor,
    AssetBundler
}