Shader "Hidden/AutoAttackIcon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FillColor ("Fill Color", Color) = (1, 1, 1, 1)
        _BackgroundColor ("Background Color", Color) = (1, 1, 1, 1)
        _FxColor ("Effect Color", Color) = (1, 1, 1, 1)
        _FillAmount ("Fill Amount", Range(0, 1)) = 0.5
        _FxTex ("Effect Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags{
            "Queue" = "Overlay"
            "RenderType" = "Overlay"
            "ForceNoShadowCasting" = "True"
        }

        Blend SrcAlpha OneMinusSrcAlpha

        Cull Off ZWrite Off ZTest Always

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
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _FxTex;
            float4 _FillColor;
            float4 _BackgroundColor;
            float4 _FxColor;
            float _FillAmount;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float4 fx = tex2D(_FxTex, i.uv + float2(0, -_Time.y)) * col.a;
                float fill = i.uv.y <= _FillAmount;
                col = col.a * (_FillColor * fill + _BackgroundColor * (1 - fill)) + fx * fill * _FxColor;
                return saturate(col);
            }
            ENDCG
        }
    }
}
