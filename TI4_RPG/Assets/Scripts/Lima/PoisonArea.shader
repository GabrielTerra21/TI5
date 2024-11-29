Shader "Unlit/PoisonArea"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1, 1, 1, 1)
        _Color2 ("Color 2", Color) = (1, 1, 1, 1)
        _Frequency ("Sine Wave Frequency", float) = 1
        _Amplitude (" Sine Wave Amplitude", float) = 1
    }
    SubShader
    {
        Tags{
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
            "ForceNoShadowCasting" = "True"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off


        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #define RAD2DEGREE(radians) ((radians) * ((180) / (PI)))
            #define INRADIUS(coord, radius) (length((coord) - (0.5, 0.5)) < (radius))

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color1;
            float4 _Color2;
            float _Frequency;
            float _Amplitude;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float2 modUv = i.uv * 2 - 1;
                
                float angleRad = atan2(modUv.x, modUv.y) / PI;
                angleRad = angleRad;
                float limitDistortion = sin((_Time.x * 3 + angleRad) * 15 * PI) * 0.01 - 0.01;
                float limitDistortion2 = sin((_Time.x * 2 + angleRad) * 15 * PI) * 0.01 - 0.01;
                float limit = INRADIUS(i.uv, 0.5 + limitDistortion);
                float limit2 = INRADIUS(i.uv, 0.4 + (limitDistortion2));
                float4 col = lerp(_Color1, _Color2, limit - limit2);
                return saturate(float4(col.xyz, limit)); 
            }

            ENDHLSL
        }
    }
}
