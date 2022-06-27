using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

/**
* @classdesc ResolutionRT
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
[RequireComponent(typeof(Camera))]
public class ResolutionRT : MonoSingleton<ResolutionRT>
{
   
        public enum Resolution
        {
                [Description("无")]
                NONE,
                [Description("标清")]
                SD,
                [Description("高清")]
                HD,
                // [Description("高清")]
                // WHD,
                [Description("全高清")]
                FHD
        }

        private Dictionary<int, Vector2Int> m_ResolutionDic = new Dictionary<int, Vector2Int>
        {
                {(int)Resolution.SD,new Vector2Int(540, 960)}, // QHD
                {(int)Resolution.HD,new Vector2Int(720, 1280)}, // 720P
                //{(int)Resolution.WHD,new Vector2(768, 1366)},//WXGA 
                {(int)Resolution.FHD,new Vector2Int(1080, 1920)},// 1080P
        };
        
        public Resolution m_Resolution = Resolution.NONE;

        private Camera m_Camera;

        private RenderTexture m_FrameBuffer;

        private CommandBuffer m_CommandBuffer;

        private void Awake()
        {
                Camera c  = GetComponent<Camera>();
                Init(c,m_Resolution);
        }

        private void Refresh()
        {
                Camera c  = GetComponent<Camera>();
                Init(c,m_Resolution);  
        }
        private void OnValidate()
        {
#if UNITY_EDITOR
                Refresh();
#endif

        }
        
        public void Init(Camera cam,Resolution resolution)
        {
                m_Camera = cam;
                m_Resolution = resolution;
                ApplySolution(m_Resolution);
        }
        


        private void ApplySolution(Resolution mResolution)
        {
                m_Resolution = mResolution;
                if (mResolution == Resolution.NONE)
                {
                        return;
                }

                var size = GetResolutionSize(mResolution);
                bool isPortrait = Screen.currentResolution.width < Screen.currentResolution.height;
                var maxW = isPortrait ? size.x : size.y;
                var maxH = isPortrait ? size.y : size.x;
                size = FitScreenAspect(Screen.width, Screen.height,maxW,maxH);
                Debug.Log("init :"+size.x + "  ---"+ size.y);
                Debug.Log("sceen :"+ Screen.fullScreen+" "+Screen.width + "  ---"+ Screen.height+" --- "+ Screen.currentResolution.width);
                
                // UpdateFrameBuffer(size.x, size.y,24);
                // UpdateCameraTarget();
                RenderTexture frameBuffer = m_FrameBuffer;
                CreateRenderTexture(size.x, size.y, 24, ref frameBuffer);
                RenderCommand(m_FrameBuffer,m_Camera,m_CommandBuffer,size.x, size.y,24);
        }

        private void CreateRenderTexture(int width, int height,int depth, ref RenderTexture m_FrameBuffer, RenderTextureFormat format = RenderTextureFormat.Default)
        {
                #region CreateRT

                if (m_FrameBuffer != null)
                {
                        m_FrameBuffer.Release(); 
                        DestroyImmediate(m_FrameBuffer);
                }
                m_FrameBuffer = new RenderTexture(width, height, depth, format);
                m_FrameBuffer.useMipMap = false;
                m_FrameBuffer.Create();
                m_Camera.SetTargetBuffers(m_FrameBuffer.colorBuffer,m_FrameBuffer.depthBuffer );
                
                #endregion  
        }
        
        /// <summary>
        /// rebuild commandbuffer after Everything 
        /// </summary>
        private void RenderCommand(RenderTexture m_FrameBuffer,Camera m_Camera, CommandBuffer m_CommandBuffer,int width, int height,int depth,RenderTextureFormat format = RenderTextureFormat.Default)
        {

                // #region CreateRT
                //
                // if (m_FrameBuffer != null)
                // {
                //         m_FrameBuffer.Release(); 
                //         DestroyImmediate(m_FrameBuffer);
                // }
                // m_FrameBuffer = new RenderTexture(width, height, depth, format);
                // m_FrameBuffer.useMipMap = false;
                // m_FrameBuffer.Create();
                // m_Camera.SetTargetBuffers(m_FrameBuffer.colorBuffer,m_FrameBuffer.depthBuffer );
                //
                // #endregion 
                

                #region addCommandBuffer

                if (m_CommandBuffer != null && m_Camera != null)
                {
                        m_Camera.RemoveCommandBuffer(CameraEvent.AfterEverything,m_CommandBuffer);
                        m_CommandBuffer = null;   
                }

                m_CommandBuffer = new CommandBuffer();
                m_CommandBuffer.name = "CameraRT buffer";
                m_CommandBuffer.SetRenderTarget(-1);
                m_CommandBuffer.Blit(m_FrameBuffer,BuiltinRenderTextureType.CurrentActive);
                m_Camera.AddCommandBuffer(CameraEvent.AfterEverything,m_CommandBuffer);
                

                #endregion        
                
        }
        

        private Vector2Int FitScreenAspect(int width, int height, int maxWidth, int maxHeight)
        {

                if (width < maxWidth && height < maxHeight)
                        return new Vector2Int(width, height);
                if (width > height)
                {
                        float aspect = height / (float)width;
                        int w = Mathf.Min(width, maxWidth);
                        int h = Mathf.RoundToInt(w * aspect);
                        return new Vector2Int(w, h);
                }
                else
                {
                        float aspect = width / (float)height;
                        int h = Mathf.Min(height, maxHeight);
                        int w = Mathf.RoundToInt(height * aspect);
                        return new Vector2Int(w, h);
                }
        }

        private Vector2Int GetResolutionSize(Resolution mResolution)
        {
                m_ResolutionDic.TryGetValue((int) mResolution, out Vector2Int resolutionSize);
                if (resolutionSize == default)
                {
                        return new Vector2Int(Screen.width, Screen.height);
                }

                return resolutionSize;
        }

        // private float lastwidth;
        // private float lastheight;
        // private void OnGUI()
        // {
        //         if (lastwidth != Screen.width || lastheight != Screen.height)
        //         {
        //                 lastwidth = Screen.width;
        //                 lastheight = Screen.height;
        //                 print("Resolution ---:" + Screen.width + " X" + Screen.height);
        //         }
        // }

}
