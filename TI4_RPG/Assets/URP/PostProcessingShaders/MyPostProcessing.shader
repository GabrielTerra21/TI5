Shader "Unlit/MyPostProcessing"
{
    Properties{
        _PixelRange ("Pixel Range", Range(1, 256) ) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        ZWrite Off Cull Off
        Pass
        {
            Name "ColorBlitPass"

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            #pragma vertex Vert
            #pragma fragment frag

            TEXTURE2D_X(_CameraOpaqueTexture);
            TEXTURE2D_X(_MainTex);

            SAMPLER(sampler_CameraOpaqueTexture);
            SAMPLER(sampler_MainTex);

            float _PixelRange;


            float4 frag (Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                float2 uv = floor(input.texcoord * _PixelRange);
                uv /= _PixelRange;

                float4 opaque = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, uv);

                return opaque;
            }
            ENDHLSL
        }
    }
}
