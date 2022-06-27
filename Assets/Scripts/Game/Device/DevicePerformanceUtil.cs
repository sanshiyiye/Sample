using System;
using System.Text;
using UnityEngine;

/**
* @classdesc DevicePerformanceUtil
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
public class DevicePerformanceUtil
{
    private static string[][] devicesMap = new string[][]
    {
        new string[2]{"mali","1"}, new string[2]{"Intel(R) Iris(TM) Plus Graphics 655","2"},
        


    };
    private static Devices devices;
    public static DevicePerformanceLevel GetDevicePerformanceLevel()
    {
        
        if (SystemInfo.graphicsDeviceVendorID == 32902)
        {
            return DevicePerformanceLevel.Low;
        }
        else
        {
            //CPU核心数 
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (SystemInfo.processorCount <= 2)
#elif UNITY_STANDALONE_OSX || UNITY_IPHONE
            if (SystemInfo.processorCount < 2)
#elif UNITY_ANDROID
            if (SystemInfo.processorCount <= 4)
#endif
            {
                //CPU核心数<=2判定为低端
                return DevicePerformanceLevel.Low;
            }
            else
            {
                //显存
                int graphicsMemorySize = SystemInfo.graphicsMemorySize;
                //内存
                int systemMemorySize = SystemInfo.systemMemorySize;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                if (graphicsMemorySize >= 4000 && systemMemorySize >= 8000)
                    return DevicePerformanceLevel.High;
                else if (graphicsMemorySize >= 2000 && systemMemorySize >= 4000)
                    return DevicePerformanceLevel.Mid;
                else
                    return DevicePerformanceLevel.Low;
#elif UNITY_STANDALONE_OSX || UNITY_IPHONE
            if (graphicsMemorySize >= 4000 && systemMemorySize >= 8000)
                    return DevicePerformanceLevel.High;
                else if (graphicsMemorySize >= 2000 && systemMemorySize >= 4000)
                    return DevicePerformanceLevel.Mid;
                else
                    return DevicePerformanceLevel.Low;
#elif UNITY_ANDROID
            if (graphicsMemorySize >= 6000 && systemMemorySize >= 8000)
                    return DevicePerformanceLevel.High;
                else if (graphicsMemorySize >= 2000 && systemMemorySize >= 4000)
                    return DevicePerformanceLevel.Mid;
                else
                    return DevicePerformanceLevel.Low;
#endif
            }
        }
    }

    private static void JudgeCpu()
    {
        // throw new NotImplementedException();
    }

    

    public static void ModifySettingsBasedOnPerformance()
    {
        if (devices == null)
        {
            devices = new Devices(devicesMap);
        }
        DevicePerformanceLevel? lv = devices.DeviceLevelOfGpu(GPU());
        ModifySettingsBasedOnPerformance(lv);
    }
    public static void ModifySettingsBasedOnPerformance(DevicePerformanceLevel? level)
    {
        level = level.HasValue? level:GetDevicePerformanceLevel();
        switch (level) 
        {
            case DevicePerformanceLevel.Low:
                SetQualitySettings(QualityLevel.Low);
                //SetApplicationSettings(QualityLevel.Low);
                break;
            case DevicePerformanceLevel.Mid:
                SetQualitySettings(QualityLevel.Mid);
                break;
            case DevicePerformanceLevel.High:
                SetQualitySettings(QualityLevel.High);
                break;
        }
    }

    private static void SetApplicationSettings(QualityLevel qualityLevel)
    {
        switch (qualityLevel)
        {
            case QualityLevel.Low:
                Application.targetFrameRate = 30;
                break;
            case QualityLevel.Mid:
                Application.targetFrameRate = 30;
                break;
            case QualityLevel.High:
                Application.targetFrameRate = 60;
                break;
        }
        
    }

    public static void SetQualitySettings(QualityLevel qualityLevel)
    {
        switch (qualityLevel)
        {
            
            case QualityLevel.Low:
                //前向渲染使用的像素灯的最大数量，建议最少为1
                QualitySettings.pixelLightCount = 1;
                //你可以设置使用最大分辨率的纹理或者部分纹理（低分辨率纹理的处理开销低）。选项有 0_完整分辨率，1_1/2分辨率，2_1/4分辨率，3_1/8分辨率
                QualitySettings.masterTextureLimit = 1;
                //设置抗锯齿级别。选项有​​ 0_不开启抗锯齿，2_2倍，4_4倍和8_8倍采样。
                QualitySettings.antiAliasing = 0;
                //
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                //是否使用粒子软融合
                QualitySettings.softParticles = false;
                //启用实时反射探针，此设置需要用的时候再打开
                QualitySettings.realtimeReflectionProbes = false;
                //如果启用，公告牌将面向摄像机位置而不是摄像机方向。似乎与地形系统有关，此处没啥必要打开
                QualitySettings.billboardsFaceCameraPosition = false;
                //设置软硬阴影是否打开
                QualitySettings.shadows = ShadowQuality.Disable;
                //级联阴影设置
                QualitySettings.shadowCascades = 0;
                //
                QualitySettings.shadowDistance = 40;
                //
                QualitySettings.shadowResolution = ShadowResolution.Low;
                //
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                // //设置垂直同步方案，VSyncs数值需要在每帧之间传递，使用0为不等待垂直同步。值必须是0，1或2。
                QualitySettings.vSyncCount = 1;
                // //
                QualitySettings.lodBias = 0.5f;
                //
                QualitySettings.particleRaycastBudget = 0;
                
                break;
            case QualityLevel.Mid:
                QualitySettings.pixelLightCount = 4;
                QualitySettings.masterTextureLimit = 1;
                QualitySettings.antiAliasing = 2;
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
                QualitySettings.softParticles = false;
                QualitySettings.realtimeReflectionProbes = true;
                QualitySettings.billboardsFaceCameraPosition = false;
                QualitySettings.shadows = ShadowQuality.HardOnly;
                QualitySettings.shadowCascades = 2;
                QualitySettings.shadowDistance = 5;
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                QualitySettings.vSyncCount = 1;
                QualitySettings.lodBias = 0.5f;
                QualitySettings.particleRaycastBudget = 0;
                break;
            case QualityLevel.High:
                QualitySettings.pixelLightCount = 4;
                QualitySettings.masterTextureLimit = 0;
                QualitySettings.antiAliasing = 8;
                QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                QualitySettings.softParticles = true;
                QualitySettings.realtimeReflectionProbes = true;
                QualitySettings.billboardsFaceCameraPosition = false;
                QualitySettings.shadows = ShadowQuality.All;
                QualitySettings.shadowDistance = 5;
                QualitySettings.shadowResolution = ShadowResolution.High;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                QualitySettings.vSyncCount = 1;
                QualitySettings.lodBias = 0.5f;
               
                QualitySettings.particleRaycastBudget = 0;
                break;
        }
    }

    public void SetShadowQuality()
    {
        
    }

    public static String CPU()
    {
        
        return SystemInfo.processorType.ToString();
    }

    public static String GPU()
    {
        return SystemInfo.graphicsDeviceName;
    }
}



public enum DevicePerformanceLevel
{
    Low,
    Mid,
    High
}

public enum QualityLevel
{
    Low,
    Mid,
    High
}