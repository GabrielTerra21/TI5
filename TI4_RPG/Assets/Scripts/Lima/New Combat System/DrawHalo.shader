Shader "Unlit/DrawHalo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Thickness ("Aura Thickness", Range(0, 0.5)) = 0.4
        _FillAmount("Fill Amount", Range(0, 1)) = 1
        _Rotation ("Roation", Range(0, 360)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"  = "Transparent"}
        ZWrite On
        
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
            float4 _MainTex_ST;
            half4 _Color;
            half _Thickness;
            half _FillAmount;
            half _Rotation;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                float radians = _Rotation * UNITY_PI / 180; 

                float s = sin(radians);
                float c = cos(radians);

                float2x2 rotMat = float2x2(c, -s,
                                           s , c );
                
                float2 rotationUV = o.uv.xy * 2 - 1;
                rotationUV = mul(rotMat, rotationUV);
                o.uv = rotationUV * 0.5 + 0.5;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = length(i.uv - float2(0.5, 0.5));
                float val  = dist< 0.5 && dist> (0.5 - _Thickness);

                float2 uvCenter = i.uv.xy * 2 - 1;
                float rad = atan2(uvCenter.y, uvCenter.x) / (UNITY_PI);
                rad = step(rad * 0.5 + 0.5, _FillAmount);
                
                return (_Color * val) * rad;
            }
            ENDCG
        }
    }
}
