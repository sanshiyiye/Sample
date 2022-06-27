/*
* @classdesc ProjectorRT
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using Packages.FrameWork.Component;
using UnityEngine;
using UnityEngine.UI;

public class ProjectorRT : MonoBehaviour
{
    public int m_ShadowTextureSize = 512;
    public Camera mShadowCam;// 专门用来产生projector shadow的摄像机
    public static float ShadowTextureSizeFactor = 1;
    public Button button;
    public Material m_ShadowMaterial;
    public Image imageShow;
    public LayerMask ShadowLayerMask;
    public float m_ShadowAreaSize = 10f;  
    public Color m_ShadowColor = Color.black;  
    public static int ms_ShadowCullingMask;				// 指定将会产生阴影的物体所在层
    public float m_ShadowFarClipPlane = 100f;
    public Texture2D m_ShadowMaskTexture;
    private Projector mProjector;
    public Shader m_ShadowShader;
    public Shader m_ReplacementShader;					// 用来执行replacement render的那个shader
    public string m_ReplacementRederTag = "MyProjectorShadow";			// 用来执行replacement render的那个shader的名字

    private bool m_Initialized = false;
    private void Start()
    {
        if (button)
        {
            UIClickLisener.Get(button, OnClick);
        }
        ms_ShadowCullingMask = 1556480;
       
    }
    
    private void Init()
    {
        if (m_Initialized)
            return;
 
        m_Initialized = true;
 
        int layer = 1 << LayerMask.NameToLayer("GameObj");

        // 指定能使用projector投射阴影的各个层
        ms_ShadowCullingMask = layer;
        
 
 
       
    }
    
    private void CreateShadow()
    {
        // 在创建阴影之前，首先查询下摄像机在不在，如果不在的话就创建
        mShadowCam = GetComponent<Camera>();
 
        if (!mShadowCam)
            mShadowCam = gameObject.AddComponent<Camera>();
 
        SetCameraValue();
        mProjector = GetComponent<Projector>();
 
        if (!mProjector)
            mProjector = gameObject.AddComponent<Projector>();
 
        SetProjectorValue();
    }
    

    public void OnClick(MonoBehaviour go)
    {

        SetCameraValue();
        SetProjectorValue();
        
        // mProjector.ignoreLayers = 
        
        // mShadowCam.targetTexture = mShadowRT;
        // mShadowCam.Render();
        //
        // RenderTexture.active = mShadowRT;
        // Texture2D screenShot = new Texture2D(mRenderTexSize, mRenderTexSize, TextureFormat.R8, false);
        // imageShow.sprite = Sprite.Create(screenShot, new Rect(0, 0, mRenderTexSize, mRenderTexSize), new Vector2 (0.5f,0.5f));
        // imageShow.rectTransform.sizeDelta = new Vector2(mRenderTexSize, mRenderTexSize);
        
        /*方法解释
        1.Tags可以自定义。
        2.subshader标签的使用：
        原本的shader：
        Tags { “RenderType”=“Opaque” }
        替换的shader：
        Tags { “RenderType”=“Opaque” }
        Tags { “RenderType”=“SomethingElse” }

        标签为"RenderType"=“Opaque"的，会被同样是"RenderType”="Opaque"标签的替换shader所替换。需要替换的shader，RenderType后面的键值要一样。
        官网文档解释：SetReplacementShader将浏览场景中的所有对象，不是使用其常规着色器，而是使用第一个子着色器，该子着色器具有与指定关键帧匹配的值。 
        其Shader具有Rendertype =“ Opaque”标记的对象将被替换的shader中的第一个具有Rendertype =“ Opaque”的subshader替换，
        任何具有RenderType =“ SomethingElse”着色器的对象将使用第二个具有RenderType =“ SomethingElse”的替换子着色器，依此类推。 如果着色器的
        替换着色器中的指定关键帧的着色器没有匹配的标记值，则将不呈现任何对象。*/ 
        mShadowCam.SetReplacementShader(m_ReplacementShader, m_ReplacementRederTag);

    }

    private void SetProjectorValue()
    {
        Projector m_ShadowProjector = GetComponent<Projector>();
        m_ShadowProjector.orthographic = true;
        m_ShadowProjector.material = SetShadowMaterial();
        m_ShadowProjector.ignoreLayers = ShadowLayerMask;
        m_ShadowProjector.orthographicSize = m_ShadowAreaSize;
        m_ShadowProjector.farClipPlane = m_ShadowFarClipPlane;
    }

    private Material SetShadowMaterial()
    {
        if (!m_ShadowMaterial)
        {
           m_ShadowMaterial = new Material(m_ShadowShader)
            {
                name = "ShadowMaterialTemp"
             };
        }
        m_ShadowMaterial.SetColor("_Color", m_ShadowColor);
        m_ShadowMaterial.SetTexture("_ShadowTex", mShadowCam.targetTexture);
        m_ShadowMaterial.SetTexture("_FalloffTex", m_ShadowMaskTexture);
        return m_ShadowMaterial;
    }


    public void SetCameraValue()
    {
        mShadowCam = gameObject.AddComponent<Camera>();
        mShadowCam.clearFlags = CameraClearFlags.SolidColor;
        mShadowCam.backgroundColor = Color.black;
        mShadowCam.allowHDR = false;
        mShadowCam.allowMSAA = false;
        mShadowCam.orthographic = true;
        mShadowCam.orthographicSize = m_ShadowAreaSize;
        mShadowCam.depth = -1;
        mShadowCam.cullingMask = ms_ShadowCullingMask;
        SetRenderedTexture();
        mShadowCam.farClipPlane = m_ShadowFarClipPlane;

    }

    private void SetRenderedTexture()
    {
        if (mShadowCam == null)
            return;
        if (mShadowCam.targetTexture != null)
            RenderTexture.ReleaseTemporary(mShadowCam.targetTexture);
        RenderTextureFormat rtFormat = RenderTextureFormat.R8;
        var rt = RenderTexture.GetTemporary((int)(m_ShadowTextureSize * ShadowTextureSizeFactor),
        (int)(m_ShadowTextureSize * ShadowTextureSizeFactor), 0);
        rt.name = "ShadowRenderTemp";
        rt.format = rtFormat;
        rt.antiAliasing = 1;
        rt.filterMode = FilterMode.Bilinear;
        rt.wrapMode = TextureWrapMode.Clamp;
        mShadowCam.targetTexture = rt;
        if (m_ShadowMaterial != null)
            m_ShadowMaterial.SetTexture("_ShadowTex", mShadowCam.targetTexture);
        
    }
}
