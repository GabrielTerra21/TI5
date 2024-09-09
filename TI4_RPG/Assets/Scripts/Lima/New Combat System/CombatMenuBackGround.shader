Shader "Unlit/CombatMenuBackGround"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Noise ("Noise Texture", 2D) = "white" {} 
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Side ("Side", float) = 0
    }
    SubShader
    {
        Tags { 
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True" 
        }
        
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define PI 3.141592

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _Noise;
            float4 _MainTex_ST;
            float4 _Noise_ST;
            float4 _Color;
            float _Side;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float x =abs(_Side - i.uv.x);
                float y = i.uv.y ;
                float val =  step( x  + y , 1);
                return _Color * saturate(val);
            }
            ENDHLSL
        }
    }
}
