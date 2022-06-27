using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
* @classdesc DeviceInfo
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

[CreateAssetMenu()]
public class DeviceInfo : ScriptableObject
{

        private static DeviceInfo _instance;

        [SerializeField]
        private  string DEVICEINFO_PATH = "Assets/Resources/DeviceInfo.asset";
        [SerializeField]
        public List<string> lowLevelDevices;

        [SerializeField]
        public List<string> midLevelDevices;
        
        [SerializeField]
        public List<string> highLevelDevices;
        
        
        public static DeviceInfo Instance
        {
                get
                {
                        if (_instance == null)
                        { 
                                _instance = Resources.Load("DeviceInfo") as DeviceInfo;  
                        }

                        if (_instance == null)
                        {
                                Debug.LogError("Load DeviceInfo Error!");
                        }
                        
                        return _instance;
                }
        }
        
        
        
}
