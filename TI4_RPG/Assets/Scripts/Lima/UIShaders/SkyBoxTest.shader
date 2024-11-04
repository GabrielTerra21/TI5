Shader "Unlit/SkyBoxTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Top Color", Color) = (1, 1, 1, 1)
        _Color2 ("Bottom Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags {
            "Queue" = "Background" 
            "RenderType" = "Background"
            "PreviewType" = "Skybox"
        }
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float normalizedObjectPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color1;
            float4 _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = TransformObjectToHClip(v.vertex);
                o.normalizedObjectPos = normalize((v.vertex + 1) /2).y;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return lerp(_Color2, _Color1, i.normalizedObjectPos);
            }
            
            ENDHLSL
        }
    }
}
