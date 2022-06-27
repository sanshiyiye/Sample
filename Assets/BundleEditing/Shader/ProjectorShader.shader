Shader "Custom/Projectorshader"
{

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        
        PASS
        {
            
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM

            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            struct appdata
            {
                float4 vertex : POSITION ;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 uvShadow : TEXCOORD0;
                float4 uvFalloff : TEXCOORD1;
                half3 wNormal : TEXCOORD2;
                
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4x4 unity_Projector;
            float4x4 unity_ProjectorClip;
            fixed4 _Color;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uvShadow = mul(unity_Projector, v.vertex);
                o.uvFalloff = mul(unity_ProjectorClip, v.vertex);
                o.wNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                half3 L = normalize(_WorldSpaceLightPos0.xyz);
                half3 N = normalize(i.wNormal);
                half fade = max(0,dot(L,N));
                fade += saturate(1- i.uvFalloff.x);
                fixed shadow = tex2Dproj(_MainTex,UNITY_PROJ_COORD(i.uvShadow));
                return shadow* _Color * fade;
                
            }
            ENDCG
        }
       
    }
    FallBack "Diffuse"
}
