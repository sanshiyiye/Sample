/**
* @classdesc PathUtils
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public partial class PathUtils
{
    /// <summary>
    /// 获取assetbundle的name
    /// </summary>
    /// <param name="path">绝对路径</param>
    /// <param name="root">资源文件夹根目录</param>
    /// <returns></returns>
    public static string GetAssetBundleNameWithPath(string path, string root)
    {
        string temp = NormalizePath(path);
        temp = ReplaceFirst(temp, root + "/", "");
        return temp;
    }
    /// <summary>
    /// 替换第一个符号
    /// </summary>
    /// <param name="str"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private static string ReplaceFirst(string str, string oldValue, string newValue)
    {
        int index = str.IndexOf(oldValue);
        str = str.Remove(index, oldValue.Length);
        str = str.Insert(index, newValue);
        
        return str;
    }
    /// <summary>
    /// 转换路径中的 "\" 为"/" 符号
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string NormalizePath(string path)
    {
        return path.Replace(@"\", "/");
    }
}
