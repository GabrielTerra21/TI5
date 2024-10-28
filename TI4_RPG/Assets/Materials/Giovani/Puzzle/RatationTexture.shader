Shader "Unlit/RatationTexture"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Opp("Operation", Float) = 0

        _MainTex ("Texture", 2D) = "white" {}
        _MainTex2 ("Texture", 2D) = "white" {}
        [HDR]_Color ("Color", Color) = (1, 1, 1, 1)
        _Rotation("Rotation", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend [_SrcFactor] [_DstFactor]
        BlendOp [_Opp]
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _MainTex2;
            float4 _MainTex_ST, _MainTex2_ST, _Color;
            float _Rotation;

            float2 RotateMat (float2 uv, float t)
            {
                float2 newUV;
                newUV = uv * 2 - 1;
                float c =  cos(_Rotation + t);
                float s =  sin(_Rotation + t);
                float2x2 mat = float2x2(c, -s, s, c);
                newUV = mul(mat, newUV);
                newUV = newUV * 0.5 + 0.5;
                return newUV;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv.xy, _MainTex);
                o.uv.xy = RotateMat(o.uv.xy, _Time.y);
                o.uv.zw = RotateMat(v.uv.xy, -_Time.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv.xy) * _Color;
                fixed4 col2 = tex2D(_MainTex2, i.uv.zw) * _Color;
                return float4(col.rgb * 0.5 + col2.rgb * 0.5, col.a);
            }
            ENDCG
        }
    }
}
