Shader "Unlit/HealthBar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FxTex ("Effect Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1, 1, 1, 1)
        _Color2 ("Color 2", Color) = (0, 0, 0, 1)
        _Threshold ("Gradient Threshold", Range (0, 2)) = 1
        _Attenuation ("Effect Attenuation", Range(0, 1)) = 0.5
        _FxThreshold ("Effect Threshold", Range (0, 1)) = 0.05
    }
    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
        }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 modUv : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _FxTex;
            float4 _MainTex_ST;
            float4 _FxTex_ST;
            float4 _Color1;
            float4 _Color2;
            float _Threshold;
            float _Attenuation;
            float _FxThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.modUv = TRANSFORM_TEX(v.uv, _FxTex);
                o.modUv.x -= frac(_Time.x);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 mask = tex2D(_MainTex, i.uv);
                float4 fx = tex2D(_FxTex, i.modUv).r;
                fx = smoothstep(fx, 0, _FxThreshold) * _Attenuation; 
                float val = saturate(i.uv.x  - (fx - i.uv.x))/ _Threshold;
                float4 finalCol = lerp(_Color1, _Color2, val);
                return saturate(finalCol * mask);
            }
            ENDCG
        }
    }
}
