using UnityEngine;
 [ExecuteInEditMode]
public class ShadowProjector : MonoBehaviour
 {
  public static float ShadowTextureSizeFactor = 1;
  public float Intensity = 1f;
  public Color LightColor = Color.white;

   // 是否启用阴影
 public bool m_Shadow = false;
  public float m_ShadowAreaSize = 10f;                // 阴影的作用区域
     public Color m_ShadowColor = Color.black;           // 阴影的颜色
     public static int ms_ShadowCullingMask;				// 指定将会产生阴影的物体所在层
     public LayerMask ShadowLayerMask;
     public float m_ShadowFarClipPlane = 100f;
     public int m_ShadowTextureSize = 512;
     public Vector3 m_LightDirection;					// 光照的方向，自然也是projector的投影方向
     public Camera ShadowCamera { get; private set; }	// 专门用来产生projector shadow的摄像机
     public Material m_ShadowMaterial;
     private Projector m_ShadowProjector;				// 用来执行投影纹理的projector对象
     private bool m_Initialized = false;
     public Shader m_ReplacementShader;					// 用来执行replacement render的那个shader
     public string m_ReplacementRederTag = "MyProjectorShadow";			// 用来执行replacement render的那个shader的名字
     public Texture2D m_ShadowMaskTexture;
     public Shader m_ShadowShader;						// 绘制阴影用的shadow、
 
     public ShadowProjector()
     {
         ms_ShadowCullingMask = 1556480;
     }
 
     public void SetAllValue()
     {
         if (!m_Shadow)
             return;
 
         SetCameraValue();
         SetProjectorValue();
     }
 
     private void CreateShadow()
     {
         // 在创建阴影之前，首先查询下摄像机在不在，如果不在的话就创建
         ShadowCamera = GetComponent<Camera>();
 
         if (!ShadowCamera)
             ShadowCamera = gameObject.AddComponent<Camera>();
 
         SetCameraValue();
         m_ShadowProjector = GetComponent<Projector>();
 
         if (!m_ShadowProjector)
             m_ShadowProjector = gameObject.AddComponent<Projector>();
 
         SetProjectorValue();
     }
 
     private void DestroyShadow()
     {
         if (!ShadowCamera)
             ShadowCamera = GetComponent<Camera>();
 
         if (ShadowCamera)
         {
             if (ShadowCamera.targetTexture != null)
                 ShadowCamera.targetTexture.Release();
 
             DestroyImmediate(ShadowCamera);
         }
 
         if (!m_ShadowProjector)
             m_ShadowProjector = GetComponent<Projector>();
 
         if (m_ShadowProjector)
             DestroyImmediate(m_ShadowProjector);
     }
 
     private void Init()
     {
         if (m_Initialized)
             return;
 
         m_Initialized = true;
 
         int layer2 = 1 << LayerMask.NameToLayer("Hero");
         int layer3 = 1 << LayerMask.NameToLayer("NPC");
         int layer1 = 1 << LayerMask.NameToLayer("Monster");
 
         // 指定能使用projector投射阴影的各个层
         ms_ShadowCullingMask = layer1 | layer2 | layer3;
 
         if (m_Shadow)
             CreateShadow();
         else
             DestroyShadow();
 
 
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
        if (ShadowCamera)
            ShadowCamera.SetReplacementShader(m_ReplacementShader, m_ReplacementRederTag);
    }

    private void LateUpdate()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            return;
        }
        SetAllValue();
#endif
    }

    private void OnEnable()
    {
        Init();
    }

    private void SetCameraValue()
    {
        // ShadowCamera沿着光线的方向，用正交投影的方式，把场景中会产生
        // 影子的角色，往ShadowCamera的Target texture上投射。这时候投射
        // 的影子的外观，就和角色往场景中投下的影子的外观，是一致的。
        ShadowCamera.allowHDR = false;
        ShadowCamera.allowMSAA = false;
        ShadowCamera.orthographic = true;
        ShadowCamera.orthographicSize = m_ShadowAreaSize;
        ShadowCamera.depth = -1;
        ShadowCamera.clearFlags = CameraClearFlags.SolidColor;
        ShadowCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
        ShadowCamera.cullingMask = ms_ShadowCullingMask;
        SetRenderedTexture();
        ShadowCamera.farClipPlane = m_ShadowFarClipPlane;
    }

    private void SetProjectorValue()
    {
        m_ShadowProjector.orthographic = true;
        m_ShadowProjector.material = SetShadowMaterial();
        m_ShadowProjector.ignoreLayers = ShadowLayerMask;
        m_ShadowProjector.orthographicSize = m_ShadowAreaSize;
        m_ShadowProjector.farClipPlane = m_ShadowFarClipPlane;
    }

    public void SetRenderedTexture()
    {
        if (ShadowCamera == null)
            return;

        if (ShadowCamera.targetTexture != null)
            RenderTexture.ReleaseTemporary(ShadowCamera.targetTexture);

        RenderTextureFormat rtFormat = RenderTextureFormat.R8;
        var rt = RenderTexture.GetTemporary((int)(m_ShadowTextureSize * ShadowTextureSizeFactor),
            (int)(m_ShadowTextureSize * ShadowTextureSizeFactor), 0);

        rt.name = "ShadowRenderTemp";
        rt.format = rtFormat;
        rt.antiAliasing = 1;
        rt.filterMode = FilterMode.Bilinear;
        rt.wrapMode = TextureWrapMode.Clamp;
        ShadowCamera.targetTexture = rt;

        if (m_ShadowMaterial != null)
            m_ShadowMaterial.SetTexture("_ShadowTex", ShadowCamera.targetTexture);
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
        m_ShadowMaterial.SetTexture("_ShadowTex", ShadowCamera.targetTexture);
        m_ShadowMaterial.SetTexture("_FalloffTex", m_ShadowMaskTexture);
        return m_ShadowMaterial;
    }

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        if (ShadowCamera != null && ShadowCamera.targetTexture != null)
        {
            RenderTexture.ReleaseTemporary(ShadowCamera.targetTexture);
        }
    }
}