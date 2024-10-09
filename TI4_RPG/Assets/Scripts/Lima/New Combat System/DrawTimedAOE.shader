Shader "Unlit/DrawTimedAOE"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AoeColor ("AOE Color", color) = (1, 1, 1, 1)
        _Duration ("AOE Duration", float) = 1
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
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
            half4 _AoeColor;
            float _Duration;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half dist = length(i.uv - float2(0.5, 0.5))/ 0.5;
                half dist2 = dist / ( (frac(_Time.y) / _Duration));
                if(dist > 1)
                    dist = 0;
                if(dist2 > 1)
                    dist2 = 0;
                half val = pow(dist, 3);
                half val2 = pow(dist2, 3);
                
                return (_AoeColor * val) + (_AoeColor * val2);
            }
            ENDCG
        }
    }
}
