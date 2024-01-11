﻿Shader "Unlit/VertexShader"
{
    Properties
    {
        //_MainColor("Color", Color) = (1,0,0,1)
    }
    SubShader {
        PASS{        
        CGPROGRAM
        #pragma vertex vert  
        #pragma fragment frag

        #pragma multi_compile __ OPEN
        #include "UnityCG.cginc" //引用unity内置shader辅助函数

        //定义一个结构体用来传递位置信息和颜色
        struct v2f{
            float4 pos:POSITION;
            float4 col:COLOR;
        };  

        v2f vert(appdata_full v){
            v2f o;
            o.pos=UnityObjectToClipPos(v.vertex);
            #if OPEN
                o.pos = float4(1.0,1.0,1.0,1.0);
            #endif
            
            //取出顶点颜色
            o.col=v.color;       
            return o;
        }
        float4 frag(v2f IN):COLOR{
                return IN.col;
        }
        ENDCG
        }
    }
}
