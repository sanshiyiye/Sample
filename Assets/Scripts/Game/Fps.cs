using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

/**
* @classdesc Fps
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
public class Fps:MonoBehaviour
{

    private float timeleft;

    private float accum;

    private float frames;

    public Text tvFpsInfo;

    public float updateInterval = 0.5f;



    /// <summary>
    ///  CPU 时间
    /// </summary>
    public float CpuFrameTime { get; private set; }
    /// <summary>
    /// GPU 时间
    /// </summary>
    public float GpuFrameTime { get; private set; } 
    
    private FrameTiming[] _frameTimings = new FrameTiming[1];
    
    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;
        
        if (timeleft <= 0.0)
        {
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            tvFpsInfo.text = format;
            if (fps < 30)
            {
                tvFpsInfo.material.color = Color.yellow;
                
            }
            else if (fps < 10)
            {
                tvFpsInfo.material.color = Color.red;
            }
            else
            {
                tvFpsInfo.material.color = Color.green;
            }

            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
            
            
        }
        //
#if UNITY_ANDROID && UNITY_IPHONE
        FrameTimingManager.CaptureFrameTimings();
        var numFrames = FrameTimingManager.GetLatestTimings((uint)_frameTimings.Length, _frameTimings);
        if (numFrames == 0) 
        {
            return;
        }
        CpuFrameTime = (float)(_frameTimings[0].cpuFrameTime * 1000);
        GpuFrameTime = (float)(_frameTimings[0].gpuFrameTime * 1000);
#endif
       
       
    }
}
