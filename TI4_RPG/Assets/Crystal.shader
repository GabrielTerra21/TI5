Shader "Unlit/Crystal"
{
    Properties
    {
        _Color1 ("Color1", Color) = (1, 1, 1, 1)
        _Color2 ("Color2", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags {
            "Queue" = "Transparent" 
            "RenderType"="Opaque" 
            "IgnoreProjector" = "True"
        }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
                float3 viewDir : TEXCOORD1;
                float3 normal : NORMAL;
            };

            float4 _Color1;
            float4 _Color2;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.viewDir = GetWorldSpaceViewDir(o.vertex);
                o.normal = mul(unity_ObjectToWorld, v.normal);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float val = dot(i.normal, normalize(i.viewDir));
                return float4(val.xxx, 1);
            }
            ENDHLSL
        }
    }
}
