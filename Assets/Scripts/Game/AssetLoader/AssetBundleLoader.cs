using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/**
* @classdesc AssetBundleLoader
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public class AssetBundleLoader : IAssetLoader
{
    public string STREAMING_ASSET_AB_ROOTPATH;

    public string STREAMING_ASSET_MANIFESTPATH;

    private static AssetBundleManifest manifest;
    
    public T loadAsset<T>(string path) where T : class
    {
        string absolutepath = path;
        path = PathUtils.NormalizePath(path);
        string assetBundleName = PathUtils.GetAssetBundleNameWithPath(path,STREAMING_ASSET_AB_ROOTPATH);
        Debug.Log("[LoadAsset]: " + path);
        LoadManifest();
        string[] dependencies = manifest.GetAllDependencies(assetBundleName);
        
        return null;

    }

    private void LoadManifest()
    {
        if (manifest == null)
        {
            string path = STREAMING_ASSET_MANIFESTPATH;
            AssetBundle mainfestab = AssetBundle.LoadFromFile(path);
            manifest = mainfestab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            mainfestab.Unload(false);
        }
    }

    public IEnumerable LoadAssetAsync<T>(string path, UnityAction<T> action) where T : class
    {
        throw new System.NotImplementedException();
    }
}
