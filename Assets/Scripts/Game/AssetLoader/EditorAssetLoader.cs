using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/**
* @classdesc EditorAssetLoader
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public class EditorAssetLoader : IAssetLoader
{
    public T loadAsset<T>(string path) where T : class
    {
        return load<T>(path);
    }

    public IEnumerable LoadAssetAsync<T>(string path, UnityAction<T> callback) where T : class
    {
        if (callback != null)
        {
            callback(load<T>(path));
        }

        yield return null;
    }

    private T load<T>(string path) where T : class
    {
#if UNITY_EDITOR
        string absolutepath = path;
        Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(T));
        if (obj == null)
        {
            Debug.LogError(" Asset not found - path:"+path);
        }
        AssetManager.Instance.PushCache(absolutepath,obj);
        return obj as T;
        
#endif
        return null;

    }
}
