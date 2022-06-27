// using System;
using UnityEngine;
using UnityEngine.UI;

/**
* @classdesc GameDebug
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
public class GameDebug : MonoBehaviour
{

    public GameObject light;
    
    public GameObject lightControlBtn;

    private bool lightEnable = false;

    private bool qualitySetting = false;
    public GameObject qualitySettingBtn;
    
    public GameObject LowBtn;
    public GameObject MidBtn;
    public GameObject HighBtn;

    public GameObject antiAliasingBtn;
    
    private void Start()
    {
        lightControlBtn.GetComponent<Button>().onClick.AddListener(CloseShadow);
        antiAliasingBtn.GetComponent<Button>().onClick.AddListener(AntiAliasing);
        LowBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SetQaulity(0);
            //LowBtn.GetComponent<Button>().colors
        });
        MidBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SetQaulity(1);
        });
        HighBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            SetQaulity(2);
        });
        DevicePerformanceUtil.ModifySettingsBasedOnPerformance();
    }

    private int syncCount = 0; 
    public void CloseShadow()
    {
        
        // light.GetComponent<Light>().shadows = lightClosed ? LightShadows.None:LightShadows.Soft;
        // float distance = QualitySettings.shadowDistance;
        // Debug.LogWarning(String.Format("shadowDistance is {0},qulityLevel is {1}",distance.ToString(),QualitySettings.GetQualityLevel()));
         QualitySettings.vSyncCount = syncCount;
        syncCount = (syncCount + 1) ;
        syncCount = syncCount >= 3 ? 0 : syncCount;
        Debug.LogWarning("syncCount is "+ syncCount);
        // lightEnable = !lightEnable;

    }

    private int antiAliasingCount = 0;
    public void AntiAliasing()
    {
        QualitySettings.vSyncCount = antiAliasingCount;
        antiAliasingCount = antiAliasingCount == 0? 1<< 1:antiAliasingCount<<1;
        antiAliasingCount = antiAliasingCount > 8 ? 0 : antiAliasingCount;
        Debug.LogWarning("antiAliasingCount is "+ antiAliasingCount);
    }

    public void SetQaulity(int lv)
    {
        DevicePerformanceLevel level = DevicePerformanceUtil.GetDevicePerformanceLevel();
        // Debug.LogWarning(String.Format("Level is {0},CPU is {1},GPU is {2}",level.ToString(),DevicePerformanceUtil.CPU(),DevicePerformanceUtil.GPU()));
        QualityLevel l = (QualityLevel)System.Enum.ToObject(typeof(QualityLevel),lv);
        DevicePerformanceUtil.SetQualitySettings(l);
        
        // if (qualitySetting)
        // {
        //     DevicePerformanceUtil.SetQualitySettings(QualityLevel.Mid);
        // }
        // else
        // {
        //     DevicePerformanceUtil.SetQualitySettings(QualityLevel.Low);
        // }
        // qualitySetting = !qualitySetting;
    }
    
    
}