Shader "Unlit/MyPostProcessing"
{
    Properties{
        _Color ("Color", Color ) = (1, 1, 1, 1)
        _Color2 ("Color2", Color ) = (1, 1, 1, 1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _Frame ("Frame Mask", 2D) = "white" {}
        _FilmGrain ("Film Grain", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "ColorBlitPass"

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // The Blit.hlsl file provides the vertex shader (Vert),
            // input structure (Attributes) and output strucutre (Varyings)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            #pragma vertex Vert
            #pragma fragment frag

            TEXTURE2D_X(_CameraOpaqueTexture);
            TEXTURE2D_X(_MainTex);
            TEXTURE2D_X(_Frame);
            TEXTURE2D_X(_FilmGrain);

            SAMPLER(sampler_CameraOpaqueTexture);
            SAMPLER(sampler_MainTex);
            SAMPLER(sampler_Frame);
            SAMPLER(sampler_FilmGrain);

            float4 _Color;
            float4 _Color2;

            float _Intensity;


            float4 frag (Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float grain = SAMPLE_TEXTURE2D_X(_FilmGrain, sampler_FilmGrain, input.texcoord + float2(0, _Time.x)).r;

                float4 opaque = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.texcoord + float2(grain * _Intensity, 0));
                opaque.rgb = dot(opaque.rgb, float3(0.213, 0.715, 0.072));
                float value = opaque.r + opaque.g + opaque.b;
                opaque = lerp(_Color, _Color2, value);

                float frame = SAMPLE_TEXTURE2D_X(_Frame, sampler_Frame, input.texcoord).r;

                input.texcoord = input.texcoord * 3 - 1;
                float aim = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, input.texcoord).r;

                return ((opaque * frame) + (1 - frame) * _Color) + aim;
            }
            ENDHLSL
        }
    }
}
