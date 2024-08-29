Shader "Unlit/AOE"
{
    Properties
        {
            _MainTex ("Texture", 2D) = "white" {}
            _Color ( "Color", color) = (1, 0, 0, 1)
            _Color2 ("Color 2", Color) = (1, 1, 1, 1)
            _Radius ("Radius" , Float) = 1
            _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
            _Duration ("Duration", float) = 1.0
        }
        SubShader
        {
            Tags{"Queue" = "Transparent"}
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
                float4 _Color;
                float4 _Color2;
                float _Radius;
                float4 _Center;
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
                    float2 center = _Center.xy;
                    float4 col, col2;
                    float2 distance = i.uv - center;
                    if(length(distance) <= _Radius){
                        float c = length(distance) / _Radius;
                        col = _Color * c;
                    }
                    else{
                        col = (0, 0, 0, 0);
                    }
    
                    float size;
                    size = lerp(0, _Radius,(_Time.y/ _Duration % 1));
    
                    if(length(distance) <= size){
                        float c = length(distance) / size;
                        col2 = saturate(_Color2 * c);
                    }
                    else{
                        col2 = 0;
                    }
    
                    float4 texCol = tex2D(_MainTex, i.uv);
                    return col + col2;
                }
                ENDCG
            }
        }
}
