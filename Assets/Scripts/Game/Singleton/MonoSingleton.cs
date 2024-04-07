using System;
using UnityEngine;

/**
* @classdesc MonoSingleton
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
     private static T _instance = null;

     private bool m_bInit = false;

     public static T GetInstance()
     {
          if (_instance == null)
          {
               GameObject obj = GameObject.Find(typeof(T).Name);
               if (obj == null)
               {
                    obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
               }
               else
               {
                    _instance = obj.GetComponent<T>();
                    if (_instance == null)
                    {
                         obj.AddComponent<T>();
                    }

                    if (!_instance.m_bInit)
                    {
                         _instance.Init();
                    }
                         
               }
          }
          return _instance;
     }
     public virtual void Init()
     {
          DontDestroyOnLoad(_instance.gameObject);
          _instance.m_bInit = true;
     }

     private void OnApplicationQuit()
     {
          _instance = null;
     }
}
