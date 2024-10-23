Shader "Lima/DecalTesting"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            struct Input
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolator
            {
                float2 uv : TEXCOORD0;
                float4 clipPos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Color;

            Interpolator vert (Input v)
            {
                Interpolator o;
                o.clipPos = TransformObjectToHClip(v.vertex);
                return o;
            }

            half4 frag (Interpolator i) : SV_Target
            {
                float2 uv = i.clipPos.xy/ _ScaledScreenParams.xy;

                //sample depthTexture
                #if UNITY_REVERSED_Z
                    real depth = SampleSceneDepth(uv);
                #else
                    real depth = lerpUNITY_NEAR_CLIP_VALUE, 1, SampleSceneDepth(uv));
                #endif

                float3 worldPos = ComputeWorldSpacePosition(uv, depth, UNITY_MATRIX_I_VP);
                
                return float4(depth.xxx, 1);
            }
            ENDHLSL
        }
    }
}
