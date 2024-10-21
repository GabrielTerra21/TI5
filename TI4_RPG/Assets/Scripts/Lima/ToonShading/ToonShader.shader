Shader "Unlit/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "white" {}
        _Color ("Fiffuse Color", Color) = (1, 1, 1, 1)
        _DiffusePow ("Diffuse Power", Range(0, 10)) = 1
        _SpecularCol ("Specular Color", Color) = (1, 1, 1, 1)
        _SpecularPow ("Specular Power", Range (0, 10)) = 1
        _ShadowCol ("Shadow Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags {
            "LightMode" = "ForwardBase"
	        "PassFlags" = "OnlyDirectional"
        }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 lightDir : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NormalMap;
            float4 _Color;
            float _DiffusePow;
            float4 _SpecularCol;
            float _SpecularPow;
            float4 _ShadowCol;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = TransformObjectToWorld(v.normal);

                float3 lightDir = _MainLightPosition;
                float distance = length(lightDir);
                distance *= distance;
                o.lightDir = normalize(lightDir);

                o.viewDir = normalize(_WorldSpaceCameraPos);
                
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                
                float3 h = normalize(i.lightDir + i.viewDir);

                float NdotL = saturate(dot(i.normal, i.lightDir));
                NdotL = smoothstep(0, 0.025, NdotL);
                float3 lightval = lerp(_ShadowCol, _MainLightColor * col,NdotL);
                
                return float4(lightval, 1);
            }
            ENDHLSL
        }
    }
}
