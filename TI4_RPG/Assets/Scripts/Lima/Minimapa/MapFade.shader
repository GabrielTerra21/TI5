Shader "Unlit/MapFade"
{
    Properties
    {
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", color) = (1, 1, 1, 1)
        
        _StencilComp ("Stencil Comparison", float) = 8
        _Stencil ("Stencil ID", float ) = 0
        _StencilOp ("Stencil Operation", float) = 0
        _StencilWriteMask ("Stencil Write Mask", float) = 255
        _StencilReadMask ("Stencil Read Mask", float ) = 255
        
        _ColorMask ("Color Mask", float) = 15
        
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", float) = 0
    }
    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True" 
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
        
        Stencil{
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include  "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.worldPosition = v.vertex;
                o.vertex = UnityObjectToClipPos(o.worldPosition);
                
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.color = v.color * _Color;
                return o;
            }

            float CalculateDistance(in v2f i)
            {
                float2 center = (0.5, 0.5);
                float distance = length(i.uv - center) / 0.5;
                //if(distance > 1) distance = 0;
                return distance;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 color = (tex2D(_MainTex, i.uv) + _TextureSampleAdd) * i.color;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping( i.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif
                
                float scale = CalculateDistance(i);
                scale *= scale;
                
                return color * (1- scale);
            }
            ENDCG
        }
    }
}
