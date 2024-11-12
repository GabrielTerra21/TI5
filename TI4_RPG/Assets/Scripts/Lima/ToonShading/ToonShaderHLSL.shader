Shader "Unlit/ToonShaderHLSL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _SpecColor ("Specular Color", Color) = (1, 1, 1, 1)
        _Glossiness ("Glossiness", Float) = 32
        _AmbientColor ("Ambient Color", Color) = (0.5, 0.5, 0.5, 0.5) 
        _FresnelColor ("Fresnel Color", Color) = (1, 1, 1, 1)
        _FresnelAmount ("Fresnel Amount", Range(0, 1)) = 0.5
        
    }
    SubShader
    {
        Tags {
            "Queue" = "Geometry"
            "RenderType"="Opaque" 
         }

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float NdotL : TEXCOORD2;
                float3 viewDir : TEXCOORD4;
                float3 normal : TEXCOORD5;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _AmbientColor;
            float4 _SpecColor;
            float _Glossiness;
            float4 _FresnelColor;
            float _FresnelAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.normal = TransformObjectToWorldNormal(v.normal);
                o.viewDir = GetWorldSpaceViewDir(TransformObjectToWorld(v.vertex));
                o.NdotL = dot(_MainLightPosition,normalize(o.normal));
                
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.uv);
                i.normal = normalize(i.normal);
                i.viewDir = normalize(i.viewDir);
                float3 h = normalize(_MainLightPosition + i.viewDir);
                float NdotH = saturate(dot(i.normal, h));
                i.NdotL = smoothstep(0, 0.05f, i.NdotL);
                
                float fresnel = 1 -  dot(i.normal, i.viewDir);
                fresnel = smoothstep(_FresnelAmount - 0.01, _FresnelAmount + 0.01, fresnel);

                float specularIntensity =  pow(NdotH * i.NdotL, _Glossiness * _Glossiness);
                float specularIntensitysmooth = smoothstep(0, 0.005f, specularIntensity);
                float4 specular = saturate(specularIntensitysmooth * _SpecColor);
                
                return   tex * saturate((_MainLightColor * _Color * i.NdotL + specular ) + _AmbientColor + (fresnel * _FresnelColor));
            }
            ENDHLSL
        }
    }
}
