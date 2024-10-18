Shader "Unlit/SpinningSprite"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Color2 ("Back Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Rotation Speed", Range(0, 100)) = 1
        _Offset ("Offset Rotation", Range(0, 1)) = 1
        _FillAmount ("Fill Amount", Range(0, 1)) = 1
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
            #define TAU UNITY_TWO_PI

            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 rotUv: TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _Color2;
            fixed _Speed;
            float _Offset;
            float _FillAmount;

            v2f vert (appdata v)
            {
                v2f o;
                float offsetRad = _Offset * TAU;
                float offsetCos = cos(offsetRad);
                float offsetSin = sin(offsetRad);

                float2x2 fillMatrix = float2x2(offsetCos, - offsetSin,
                                               offsetSin, offsetCos);

                
                float cosen = cos(_Time.x * _Speed);
                float sen = sin(_Time.x * _Speed);
                    
                float2x2 rotMat = float2x2(cosen, -sen,
                                           sen, cosen);

                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                
                o.rotUv.xy = o.uv * 2 - 1;
                o.rotUv = mul(rotMat, o.rotUv.xy);
                o.rotUv.xy = o.rotUv * 0.5 + 0.5;

                
                o.uv.xy = o.uv * 2 - 1;
                o.uv = mul(fillMatrix, o.uv.xy);
                o.uv.xy = o.uv * 0.5 + 0.5;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mask = tex2D(_MainTex, i.rotUv);
                
                float2 newUvs = i.uv.xy * 2 - 1;
                float radial = atan2(newUvs.y, newUvs.x) / UNITY_PI;
                radial = radial * 0.5 + 0.5;
                float fill = step(1 - radial, _FillAmount);
                
                return mask *(fill * _Color + (1 - fill) * _Color2) ;
            }
            ENDCG
        }
    }
}
