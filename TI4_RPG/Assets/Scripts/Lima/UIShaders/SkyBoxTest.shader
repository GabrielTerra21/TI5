Shader "Unlit/SkyBoxTest"
{
    Properties
    {
        _CloudTex1 ("First Cloud Texture", 2D) = "white" {}
        _CloudTex2 ("Seccond Cloud Texture", 2D) = "white" {}
        _BackgroundTex ("Background Texture", 2D) = "white" {}
        _Color1 ("Top Color", Color) = (1, 1, 1, 1)
        _Color2 ("Bottom Color", Color) = (1, 1, 1, 1)
        _Step ("Cloud Step", Range(0, 1)) = 0
        _Speed ("Speed" , Range (0, 1)) = 1
    }
    SubShader
    {
        Tags {
            "RenderPipeline" = "Universal"
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
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float normalizedObjectPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _CloudTex1;
            sampler2D _CloudTex2;
            sampler2D _BackgroundTex;
            float4 _CloudTex1_ST;
            float4 _CloudTex2_ST;
            float4 _Color1;
            float4 _Color2;
            float _Step;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = TransformObjectToHClip(v.vertex);
                o.normalizedObjectPos = normalize((v.vertex + 1) /2).y;
                o.uv = ComputeScreenPos(o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 screenUv = i.uv.xy/ i.uv.w; 
                float4 tex1 = tex2D(_CloudTex1,screenUv + float2(frac(_Time.x * _Speed), 0));
                float4 tex2 = tex2D(_CloudTex2,screenUv + float2(-frac(_Time.x * _Speed), frac(_Time.x * _Speed)));
                float4 backdrop = tex2D(_BackgroundTex, screenUv);
                tex1 /= 2;
                tex2 /= 2;
                float4 finalCloud = step(tex1 + tex2, _Step * 0.5 );
                float4 grad = lerp( backdrop * _Color2, _Color1, saturate(i.normalizedObjectPos + finalCloud));
                return saturate(grad);
            }
            
            ENDHLSL
        }
    }
}
