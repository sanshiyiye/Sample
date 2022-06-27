Shader "Unlit/VertexShader"
{
    Properties
    {
        [HideInInspector]
        __dirty("",Int) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry+0" "IsEmissive" = "true"}
        Cull Back
        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf Unlit keepalpha addshadow fullforwardshadows
        struct Input
        {
            float4 vertexColor : COLOR;
            
        };

        void surf(Input i, inout SurfaceOutput o)
        {
            o.Emission = i.vertexColor.rgb;
            o.Alpha = 1;
        }
        ENDCG
    }
}
