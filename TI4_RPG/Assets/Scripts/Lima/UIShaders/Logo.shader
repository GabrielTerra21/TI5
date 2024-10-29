Shader "Unlit/Logo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FXTex ("Effect Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1, 1, 1, 1)
        _Color2 ("Color 2", Color) = (0, 0, 0, 1)
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _FXTex;
            float4 _MainTex_ST;
            float4 _FXTex_ST;
            float4 _Color1;
            float4 _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mask = tex2D(_MainTex, i.uv);
                float2 modUV = float2(frac(i.uv.x * 2), frac(i.uv.y - _Time.x));
                fixed fxCol = tex2D(_FXTex, modUV);
                fixed lerpVal = step(1 - (i.uv.y * 2) + fxCol, 0.5);
                fixed4 finalCol = lerp(_Color1, _Color2, lerpVal);
                
                return saturate(finalCol * mask);
            }
            ENDCG
        }
    }
}
